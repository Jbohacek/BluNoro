# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/BluNoro.ServerConsole/BluNoro.ServerConsole.csproj", "src/Presentation/BluNoro.ServerConsole/"]
COPY ["src/Application/BluNoro.Application/BluNoro.Application.csproj", "src/Application/BluNoro.Application/"]
COPY ["src/Core/BluNoro.Core.Infrastructure/BluNoro.Core.Infrastructure.csproj", "src/Core/BluNoro.Core.Infrastructure/"]
COPY ["src/Core/BluNoro.Core.Data.EF/BluNoro.Core.Data.EF.csproj", "src/Core/BluNoro.Core.Data.EF/"]
COPY ["src/Core/BluNoro.Core.Common/BluNoro.Core.Common.csproj", "src/Core/BluNoro.Core.Common/"]
COPY ["src/Core/BluNoro.Core.Contracts/BluNoro.Core.Contracts.csproj", "src/Core/BluNoro.Core.Contracts/"]
RUN dotnet restore "./src/Presentation/BluNoro.ServerConsole/BluNoro.ServerConsole.csproj"
COPY . .
WORKDIR "/src/src/Presentation/BluNoro.ServerConsole"
RUN dotnet build "./BluNoro.ServerConsole.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BluNoro.ServerConsole.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BluNoro.ServerConsole.dll"]