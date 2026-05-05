# EventsAPI Backend

## Prerequisites
- .NET 8 SDK
- Docker (optional)

## Run locally
```bash
dotnet restore
dotnet run --project EventsAPI/EventsAPI.csproj
```

Swagger is available at `/swagger`.

## Run with Docker Compose
```bash
docker compose up --build
```

Services:
- API: `http://localhost:8080`
- SQL Server: `localhost:1433`
- Redis: `localhost:6379`

## Default Seed Data
- Admin user: `admin@events.local` / `Admin@123`
- Album types and box types are seeded on startup.

## Configuration
Update these settings as needed:
- `ConnectionStrings:DefaultConnection`
- `Jwt` (issuer, audience, key)
- `Redis` (connection string, instance name)
