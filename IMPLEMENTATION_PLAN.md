# Implementační plán - BluNoro Refactoring

## Fáze 1: Okamžité opravy (Týden 1)

### Priorita 1: Bezpečnost
```bash
# 1. Vytvořit configuration management
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
```

```csharp
// Nahradit hardkódované heslo
// PŘED:
serverBuild.SetAdminUserPassword("123456");

// PO:
var adminPassword = configuration["AdminPassword"] ?? 
    throw new InvalidOperationException("AdminPassword not configured");
serverBuild.SetAdminUserPassword(adminPassword);
```

### Priorita 2: Build warnings
```csharp
// Opravit nullable warnings
public ILogger Logger { get; set; } = null!;
public string FileName { get; set; } = string.Empty;
```

### Priorita 3: Project struktura
```bash
# Reorganizovat projekty
mv "Core/BluNoro.Core.Common" "src/BluNoro.Core.Common"
mv "Core/BluNoro.Core.Contracts" "src/BluNoro.Core.Contracts"
mv "Core/BluNoro.Core.Data.EF" "src/BluNoro.Core.Data.EF"
mv "Core/BluNoro.Core.Infrastructure" "src/BluNoro.Core.Infrastructure"
```

## Fáze 2: Dependency Injection (Týden 2)

### Setup DI Container
```csharp
// Program.cs
var builder = Host.CreateApplicationBuilder(args);

// Services registration
builder.Services.AddScoped<IMessageServerManager, MessageServerManager>();
builder.Services.AddScoped<ILogger, Logger>();
builder.Services.AddDbContext<SqlLiteContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build and run
var host = builder.Build();
var server = host.Services.GetRequiredService<Server>();
server.Start();
```

## Fáze 3: Async/Await refactoring (Týden 3)

### Současný problém:
```csharp
// Blokující main thread
while (true)
{
    Thread.Sleep(100);
}
```

### Řešení:
```csharp
// Non-blocking
public async Task StartAsync(CancellationToken cancellationToken = default)
{
    _tcpServer.Start();
    
    while (!cancellationToken.IsCancellationRequested)
    {
        await Task.Delay(100, cancellationToken);
    }
}
```

## Fáze 4: Message Queue integrace (Týden 4)

### RabbitMQ implementace
```csharp
public interface IMessageQueue
{
    Task PublishAsync<T>(T message, string routingKey);
    Task SubscribeAsync<T>(string queueName, Func<T, Task> handler);
}

public class RabbitMQService : IMessageQueue
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    
    public async Task PublishAsync<T>(T message, string routingKey)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        _channel.BasicPublish(
            exchange: "blunoro.chat",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
            
        await Task.CompletedTask;
    }
}
```

### Chat message flow s RabbitMQ:
```
Client -> API Gateway -> Chat Service -> RabbitMQ -> Notification Service
                      -> RabbitMQ -> Connected Clients (SignalR)
```

## Fáze 5: Mikroslužby (Měsíc 2)

### 1. Authentication Service
```csharp
// BluNoro.Auth/Controllers/AuthController.cs
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // JWT token generation
        var token = await _authService.AuthenticateAsync(request);
        return Ok(new { Token = token });
    }
}
```

### 2. Chat Service
```csharp
// BluNoro.Chat/Controllers/ChatController.cs
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatController : ControllerBase
{
    [HttpPost("{chatId}/messages")]
    public async Task<IActionResult> SendMessage(
        Guid chatId, 
        [FromBody] SendMessageRequest request)
    {
        var message = await _chatService.SendMessageAsync(chatId, request);
        
        // Publish to message queue for real-time delivery
        await _messageQueue.PublishAsync(message, "chat.message.sent");
        
        return CreatedAtAction(nameof(GetMessage), 
            new { id = message.Id }, message);
    }
}
```

### 3. API Gateway (Ocelot)
```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "blunoro-auth",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/auth/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST" ]
    },
    {
      "DownstreamPathTemplate": "/api/{everything}",
      "DownstreamScheme": "http", 
      "DownstreamHostAndPorts": [
        {
          "Host": "blunoro-chat",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/chat/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    }
  ]
}
```

## Fáze 6: Monitoring a Observability

### 1. Health Checks
```csharp
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString)
    .AddRabbitMQ(rabbitMQConnection)
    .AddRedis(redisConnection);

app.MapHealthChecks("/health");
```

### 2. Structured Logging (Serilog)
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
    {
        IndexFormat = "blunoro-logs-{0:yyyy.MM.dd}"
    })
    .CreateLogger();
```

### 3. Metrics (Prometheus)
```csharp
// Custom metrics
private static readonly Counter MessagesCounter = Metrics
    .CreateCounter("blunoro_messages_total", "Total number of messages sent");

private static readonly Histogram MessageDuration = Metrics
    .CreateHistogram("blunoro_message_duration_seconds", "Message processing duration");

// Usage
MessagesCounter.Inc();
using (MessageDuration.NewTimer())
{
    await SendMessageAsync(message);
}
```

## Testovací strategie

### Unit Tests
```csharp
[Test]
public async Task SendMessage_ValidMessage_ShouldPersistToDatabase()
{
    // Arrange
    var chatService = new ChatService(_mockRepository.Object, _mockMessageQueue.Object);
    var message = new SendMessageRequest { Content = "Test message" };
    
    // Act
    var result = await chatService.SendMessageAsync(Guid.NewGuid(), message);
    
    // Assert
    result.Should().NotBeNull();
    _mockRepository.Verify(x => x.SaveAsync(It.IsAny<Message>()), Times.Once);
}
```

### Integration Tests
```csharp
[Test]
public async Task PostMessage_ValidRequest_ShouldReturn201()
{
    // Arrange
    using var client = _factory.CreateClient();
    var message = new { Content = "Integration test message" };
    
    // Act
    var response = await client.PostAsJsonAsync("/api/chat/messages", message);
    
    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);
}
```

## Performance optimalizace

### 1. Database indexy
```sql
-- Messages tabulka
CREATE INDEX IX_Messages_ChatId_CreatedAt ON Messages (ChatId, CreatedAt DESC);
CREATE INDEX IX_Messages_UserId ON Messages (UserId);

-- Chats tabulka  
CREATE INDEX IX_Chats_Name ON Chats (Name);
CREATE INDEX IX_ChatUsers_UserId_ChatId ON ChatUsers (UserId, ChatId);
```

### 2. Redis caching
```csharp
public async Task<Chat> GetChatAsync(Guid chatId)
{
    var cacheKey = $"chat:{chatId}";
    var cached = await _cache.GetStringAsync(cacheKey);
    
    if (cached != null)
        return JsonSerializer.Deserialize<Chat>(cached);
    
    var chat = await _repository.GetByIdAsync(chatId);
    await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(chat), 
        TimeSpan.FromMinutes(5));
    
    return chat;
}
```

### 3. SignalR pro real-time
```csharp
public class ChatHub : Hub
{
    public async Task JoinChatRoom(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"chat-{chatId}");
    }
    
    public async Task SendMessage(string chatId, string message)
    {
        // Validate and process
        await Clients.Group($"chat-{chatId}")
            .SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
    }
}
```

## Bezpečnostní implementace

### 1. JWT Authentication
```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
        };
    });
```

### 2. Rate Limiting
```csharp
services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
        httpContext => RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});
```

### 3. Input Validation
```csharp
public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000)
            .Must(BeValidContent).WithMessage("Content contains prohibited words");
            
        RuleFor(x => x.ChatId)
            .NotEmpty();
    }
    
    private bool BeValidContent(string content)
    {
        // Implement profanity filter, XSS protection, etc.
        return !string.IsNullOrWhiteSpace(content) && 
               !content.Contains("<script>", StringComparison.OrdinalIgnoreCase);
    }
}
```

## Deployment strategie

### 1. CI/CD Pipeline (.github/workflows/deploy.yml)
```yaml
name: Deploy BluNoro

on:
  push:
    branches: [ main ]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: 8.0.x
      - name: Run tests
        run: dotnet test
        
  build-and-deploy:
    needs: test
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Build images
        run: docker-compose build
      - name: Deploy to production
        run: docker-compose -f docker-compose.prod.yml up -d
```

### 2. Production docker-compose
```yaml
# docker-compose.prod.yml
version: '3.8'
services:
  nginx:
    image: nginx:alpine
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - ./nginx/prod.conf:/etc/nginx/nginx.conf
      - /etc/letsencrypt:/etc/letsencrypt
    restart: unless-stopped
    
  blunoro-api:
    image: blunoro/api:latest
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
    restart: unless-stopped
    deploy:
      replicas: 2
```

## Monitoring a Alerting

### 1. Application Insights
```csharp
services.AddApplicationInsightsTelemetry();
```

### 2. Custom alerts
```yaml
# monitoring/alerts.yml
groups:
  - name: blunoro
    rules:
      - alert: HighErrorRate
        expr: rate(http_requests_total{status=~"5.."}[5m]) > 0.1
        for: 2m
        annotations:
          summary: "High error rate detected"
          
      - alert: DatabaseConnectionFailed
        expr: up{job="postgres"} == 0
        for: 1m
        annotations:
          summary: "Database is down"
```

## Časový harmonogram

| Týden | Úkoly | Výsledek |
|-------|-------|----------|
| 1 | Bezpečnost, build fixes, základní refactoring | Funkční a bezpečná aplikace |
| 2-3 | DI, async/await, testování | Čistá architektura |
| 4-5 | RabbitMQ, Redis, SignalR | Real-time messaging |
| 6-8 | Mikroslužby, API Gateway | Škálovatelná architektura |
| 9-10 | Monitoring, CI/CD, produkční nasazení | Production-ready systém |

**Celkový čas: 2.5 měsíce**
**Investice: ~400-500 hodin práce**
**ROI: Production-ready, škálovatelný chat systém**