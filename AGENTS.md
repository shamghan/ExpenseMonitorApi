# ExpenseMonitor.API — AGENTS.md

## Architecture

Clean Architecture + CQRS (MediatR). Dependency flow:
`API → Infrastructure → Application → Domain`

- `src/ExpenseMonitor.API/` — ASP.NET Core endpoints, middleware, DI wiring
- `src/ExpenseMonitor.Application/` — MediatR commands/queries, AutoMapper profiles, FluentValidation validators, DTOs
- `src/ExpenseMonitor.Domain/` — entities, repository interfaces, exceptions, constants
- `src/ExpenseMonitor.Infrastructure/` — EF `DbContext`, SQL Server, ASP.NET Identity, Azure Blob Storage, seeders, authorization handlers

`slnx` solution at root: `ExpenseMonitor.API.slnx` (new `.slnx` format, not `.sln`).

## Framework quirks

| Quirk | Detail |
|---|---|
| Mixed TFMs | `ExpenseMonitor.API` targets **net10.0**; all other projects target **net8.0**. |
| Namespace typo | `ExpenseMonitor.Application.Restaurants.Command.CreateRestaurant` uses singular `Command` (should be `Commands`). |
| Folder name typo | `src/ExpenseMonitor.Infrastructure/Extension/` (singular) vs API's `Extensions/` (plural). |
| Skeleton projects | `src/Restaurants.*/` exist but contain only `obj/` — they are empty and not referenced by the solution. |

## Developer commands

```powershell
# Build & test (from solution root)
dotnet restore
dotnet build --no-restore
dotnet test

# Run API locally (launches Swagger at /swagger)
dotnet run --project src/ExpenseMonitor.API

# EF migrations (run from Infrastructure directory)
dotnet ef migrations add <Name> --project src/ExpenseMonitor.Infrastructure --startup-project src/ExpenseMonitor.API
```

CI runs `dotnet restore → build --no-restore → test` on PRs to `main` (.NET 10 SDK, ubuntu-latest).

## Test patterns

- **Integration tests**: `WebApplicationFactory<Program>` with mock DI replacement via `services.Replace()`
- **Auth bypass in integration tests**: `FakePolicyEvaluator` (in test project) replaces `IPolicyEvaluator` — provides a fake `Admin` identity with `NameIdentifier = "1"`
- **Unit tests**: xUnit + Moq + FluentAssertions

## Test project quirks

| Path issue | Note |
|---|---|
| `tests/ExpenseMonitor.Applications.Tets/` | Missing 's' — folder name is `Tets` not `Tests`. |
| `tests/ExpenseMonitor.API.Tests/MIddlewares/` | Capital 'I' — folder is `MIddlewares` not `Middlewares`. |

Infrastructure tests use `[assembly: InternalsVisibleTo("ExpenseMonitor.Infrastructure.Tests")]` (see `src/ExpenseMonitor.Infrastructure/assembly.cs`).

## Identity & authorization

- ASP.NET Core Identity mapped at route `api/identity` (see `Program.cs:39` / `IdentityController.cs`)
- Three custom authorization policies, all defined in `Infrastructure/Extension/ServiceCollectionExtensions.cs:34-38`:
  - `HasNationality` — requires claim `Nationality` with value `"Indian"` or `"German"`
  - `AtLeast20` — minimum age 20
  - `CreatedAtLeast2Restaurants` — user must own ≥2 restaurants
- Roles: `Owner`, `Admin` (see `Domain/Constants/UserRoless.cs`)

## EF & migrations

- DbContext: `RestaurantsDbContext` in `Infrastructure/Persistence/`
- Connection string: `RestaurantsDb` in `appsettings.Development.json` (points to local SQL Server `HPLAPTOP\ExpenseMonitorDb`)
- 7 migrations in `Infrastructure/Migrations/`, latest adds `categories` table
- DB is seeded on startup via `IRestaurantsSeeder.Seed()` in `Program.cs:30-31`

## Blob storage

- Configured via `blobStorage` section in `appsettings.Development.json`
- Local dev uses `UseDevelopmentStorage=true` (Azurite)
- Container name: `logos`

## Logging

Serilog configured in `appsettings.Development.json` — console + rolling file to `Logs/Restaurant-Api-.log`.

## Notable imports

Some controllers import from wrong namespace paths (e.g. `ExpenseMonitor.Application.Restaurants.Command.CreateRestaurant` in `RestaurantsController.cs:6`). When adding new commands/queries, check the actual namespace of existing ones to stay consistent with the typo.

## Entity relationships

- `Restaurant` owns `Address` (owned entity), has many `Dish` items, belongs to `User` (Owner)
- `User` (extends `IdentityUser`) has many `OwnedRestaurants`
- `Category` is a standalone entity
