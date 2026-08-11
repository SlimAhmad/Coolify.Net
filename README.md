<p align="center">
  <img src="assets/coolify.net-logo.svg" alt="Coolify.Net logo" width="240">
</p>

# Coolify.Net

A .NET client library for the [Coolify](https://coolify.io) self-hosting platform REST API.
`Coolify.Net` wraps every Coolify resource — servers, projects, applications, databases,
one-click services, deployments, teams, private keys, and system status — behind a small,
typed, fully async surface so you can provision and manage Coolify infrastructure from C#
without hand-writing HTTP calls.

## Features

- One typed client per Coolify resource (`Servers`, `Projects`, `Applications`, `Databases`,
  `CoolifyServices`, `Deployments`, `Teams`, `PrivateKeys`, `System`)
- `CancellationToken` support threaded through every call, end to end
- A layered exception model (`*ClientValidationException`, `*ClientDependencyException`,
  `*ClientServiceException`) so callers can distinguish "you gave it bad input" from
  "Coolify/the network failed" from "something unexpected happened"
- Two ways to consume it: a single `CoolifyClient` object for scripts/console apps, or an
  `AddCoolifyClient(...)` extension for apps that already run their own DI container
- Targets `net10.0`

## Installation

```bash
dotnet add package Coolify.Net
```

Or add a `ProjectReference` if you're building from source.

## Quick start

### Option 1 — `CoolifyClient` (no DI container required)

The simplest way to use the library. Construct one `CoolifyClient`, then call whichever
resource client you need off it:

```csharp
using Coolify.Net;
using Coolify.Net.Clients.Coolify.Net;
using Coolify.Net.Models.Foundations.Projects;

ICoolifyClient coolify = new CoolifyClient(options =>
{
    options.BaseUrl = "https://coolify.example.com";
    options.ApiToken = "<your Coolify API token>";
});

IEnumerable<Project> projects = await coolify.Projects.RetrieveAllProjectsAsync();

Project newProject = await coolify.Projects.AddProjectAsync(new Project
{
    Name = "my-project"
});
```

### Option 2 — `AddCoolifyClient` (for apps with their own DI container)

If your app already builds an `IServiceCollection` (ASP.NET Core, worker services, etc.),
register Coolify.Net alongside everything else and inject the client interface you need:

```csharp
using Coolify.Net.Extensions;

builder.Services.AddCoolifyClient(options =>
{
    options.BaseUrl = builder.Configuration["Coolify:BaseUrl"];
    options.ApiToken = builder.Configuration["Coolify:ApiToken"];
});
```

```csharp
using Coolify.Net.Clients.Servers;
using Coolify.Net.Models.Foundations.Servers;

public class ServerReportService(IServerClient serverClient)
{
    public async Task<IEnumerable<Server>> GetServersAsync() =>
        await serverClient.RetrieveAllServersAsync();
}
```

## What you get

| Client               | Use it to...                                                                 |
| -------------------- | ----------------------------------------------------------------------------- |
| `Servers`             | Provision/list/update/remove servers, validate reachability, list resources & domains |
| `Projects`            | Manage projects and their environments                                       |
| `Applications`        | Deploy apps from public/private Git repos, Dockerfiles, or Docker images; manage env vars; start/stop/restart |
| `Databases`           | Provision PostgreSQL, MySQL, MariaDB, MongoDB, Redis, ClickHouse, Dragonfly, or KeyDB databases; manage backups; start/stop/restart |
| `CoolifyServices`     | Deploy one-click services; manage env vars; start/stop/restart               |
| `Deployments`         | Trigger, monitor, and cancel deployments                                     |
| `Teams`               | Read teams and team membership                                               |
| `PrivateKeys`         | Manage SSH private keys used to connect to servers                          |
| `System`              | Check instance version, health, and enable/disable the API                   |

## Example: provisioning a Postgres database

```csharp
using Coolify.Net.Models.Foundations.Databases;

var database = await coolify.Databases.AddPostgreSqlDatabaseAsync(new PostgreSqlDatabase
{
    Name = "app-db",
    ServerUuid = server.Uuid,
    ProjectUuid = project.Uuid,
    EnvironmentName = "production",
    PostgresUser = "app_user",
    PostgresPassword = "change-me",
    PostgresDb = "app_db"
});

await coolify.Databases.StartDatabaseAsync(database.Uuid);
```

A complete, runnable provisioning example (project → environment → PostgreSQL → Redis →
website app → API app) lives in [`Coolify.Net.Provision`](Coolify.Net.Provision), modeled
as a console app you can point at your own Coolify instance via `appSettings.json`.

## Error handling

Every client method can throw one of three exception types, letting you handle each
category differently:

```csharp
try
{
    await coolify.Servers.AddServerAsync(server);
}
catch (ServerClientValidationException ex)
{
    // the input you gave was invalid — inspect ex.InnerException.Data for field errors
}
catch (ServerClientDependencyException ex)
{
    // Coolify (or the network) failed — safe to retry or surface as a dependency error
}
catch (ServerClientServiceException ex)
{
    // an unexpected error occurred — log it and treat as a bug
}
```

Every entity follows this same three-exception pattern (`{Entity}ClientValidationException`,
`{Entity}ClientDependencyException`, `{Entity}ClientServiceException`).

## Testing

```bash
dotnet test Coolify.Net.Tests.Unit
dotnet test Coolify.Net.Tests.Acceptance
```

`Coolify.Net.Tests.Unit` covers every layer (broker → foundation → processing → client) in
isolation. `Coolify.Net.Tests.Acceptance` emulates the Coolify REST API with
[WireMock.Net](https://github.com/WireMock-Net/WireMock.Net) and drives the full stack
through `CoolifyClient` end to end — no live Coolify instance required.

## License

[MIT](LICENSE)
