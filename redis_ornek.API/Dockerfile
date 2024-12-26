
# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["redis_ornek.API/redis_ornek.API.csproj", "redis_ornek.API/"]
COPY ["redis_ornek.Cache/redis_ornek.Cache.csproj", "redis_ornek.Cache/"]
COPY *.sln .
RUN dotnet restore "redis_ornek.API/redis_ornek.API.csproj"
COPY . .
WORKDIR "/src/redis_ornek.API"
RUN dotnet build "./redis_ornek.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./redis_ornek.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "redis_ornek.API.dll"]