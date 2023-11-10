# ConfSystem
ConfSystem is project using modular monolith architecture written in <b>.NET 7.0<b/>. The domain revolves around the conference organization including the activities such as managing users, speakers, tickets, call for papers, submissions, agendas and many more.
<br/>
<br/>
Depending on the module complexity, different architectural styles are being used, including simple CRUD approach, along with CQRS, Clean Architecture and Domain-Driven Design with the usage of so-called building blocks such as aggregates or domain services.

## Stack & Technologies:
- C# 12
- .NET 7.0
- Entity Framework Core
- PostgreSQL
- Docker

## Database
The project uses a Postgres database using Docker. Docker settings are in the <b>docker-compose.yml</b> file.
For the database to work properly, create a migration and apply the appropriate data in the file <b>appsettings.json</b> in the Bootstraper project on line 19 <b>"connectionString"</b> change the database path to your database path.

**How to start the solution?**
----------------

Type the following command:

```
docker-compose up -d
```

It will start the required infrastructure using [Docker](https://docs.docker.com/get-docker/) in the background. Then, you can start the application under `src/Bootstrapper/Confab.Bootstrapper/` using your favorite IDE or CLI.

```
cd src/Bootstrapper/ConfSystem.Bootstrapper
dotnet run
```
