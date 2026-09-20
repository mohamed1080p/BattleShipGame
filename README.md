# BattleShipGame — Dev Environment Setup

Multiplayer Battleship game server built with ASP.NET Core (.NET 10) and raw WebSockets, using Clean Architecture.

## Prerequisites

- .NET SDK 10
- Docker (for PostgreSQL)

## Setup

1. **Clone the repo**
   ```
   git clone https://github.com/mohamed1080p/BattleShipGame.git
   cd BattleShipGame
   ```

2. **Restore dependencies**
   ```
   dotnet restore
   ```

3. **Start PostgreSQL**
   ```
   docker compose up -d
   ```

4. **Set local secrets** (values shared separately — never commit these)
   ```
   dotnet user-secrets init --project BattleShipGame.Api
   dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=battleshipgame;Username=postgres;Password=postgres" --project BattleShipGame.Api
   dotnet user-secrets set "Jwt:Key" "<shared-secret>" --project BattleShipGame.Api
   ```

5. **Apply migrations**
   ```
   dotnet ef database update --project BattleShipGame.Infrastructure --startup-project BattleShipGame.Api
   ```

6. **Run**
   ```
   dotnet run --project BattleShipGame.Api
   ```

## Solution structure

```
BattleShipGame.sln
├── BattleShipGame.Domain/           → entities & pure game rules, no dependencies
├── BattleShipGame.Application/      → orchestration + interfaces
├── BattleShipGame.Infrastructure/   → EF Core, PostgreSQL, JWT/password hashing
├── BattleShipGame.Contracts/        → shared DTOs / message records
└── BattleShipGame.Api/              → REST controllers, WebSocket transport, composition root
```

## Branching

GitHub Flow: `main` stays stable, one branch per feature slice (e.g. `feature/auth`, `feature/game-domain`), merge back when the slice works.
