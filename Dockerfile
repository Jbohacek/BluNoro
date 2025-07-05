# Dockerfile pro BluNoro Server
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["BluNoro.Server/BluNoro.ServerConsole.csproj", "BluNoro.Server/"]
COPY ["BluNoro.Core/BluNoro.Core.csproj", "BluNoro.Core/"]
COPY ["Core/BluNoro.Core.Common/BluNoro.Core.Common.csproj", "Core/BluNoro.Core.Common/"]
COPY ["Core/BluNoro.Core.Contracts/BluNoro.Core.Contracts.csproj", "Core/BluNoro.Core.Contracts/"]
COPY ["Core/BluNoro.Core.Data.EF/BluNoro.Core.Data.EF.csproj", "Core/BluNoro.Core.Data.EF/"]
COPY ["Core/BluNoro.Core.Infrastructure/BluNoro.Core.Infrastructure.csproj", "Core/BluNoro.Core.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "BluNoro.Server/BluNoro.ServerConsole.csproj"

# Copy source code
COPY . .

# Build application
WORKDIR "/src/BluNoro.Server"
RUN dotnet build "BluNoro.ServerConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BluNoro.ServerConsole.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "BluNoro.ServerConsole.dll"]