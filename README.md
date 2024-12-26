# Redis Example API

This is a sample .NET 8 API project created to demonstrate the use of Redis for caching using the StackExchange.Redis library. The project includes a simple API application where data is fetched from Redis cache.

## Prerequisites

- Docker

## Getting Started

### Clone the repository
`git clone https://github.com/erenbekmezci/redis_ornek.API.git`

`cd redis_ornek.API`

### Build and Run with Docker
`docker compose up --build`

The application will start on `http://localhost:5000`.

Swagger is used for API documentation. Once the application is running, you can access the Swagger UI at `http://localhost:5000/swagger`.

### Configuration

The Redis connection URL should be configured in the `appsettings.json` file under `CacheOptions:Url`.

`{ "CacheOptions": { "Url": "redis:6379" } }`

## Dependencies

- Microsoft.EntityFrameworkCore
- StackExchange.Redis








