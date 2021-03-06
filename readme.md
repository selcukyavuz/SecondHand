## Run Locally đ¨âđģ

đ§ Requirement

 - Docker https://docs.docker.com/desktop/windows/install/
 - .net 6 SDK https://dotnet.microsoft.com/en-us/download/dotnet/6.0
 - VS Code https://code.visualstudio.com/download

âŦ  Clone the project
```bash
git clone https://github.com/selcukyavuz/SecondHand.git
```
đ Open folder with VS Code
```bash
code .
```
đ Enter the Credentials
```bash
dotnet user-secrets -p src/SecondHand.Api set "ConnectionStrings:DefaultConnection" ,"Server=127.0.0.1,1436;Database=SecondHand;user=sa;password=Password123;MultipleActiveResultSets=true;Trust Server Certificate=true;"
dotnet user-secrets -p src/SecondHand.Api set "SecondHandDatabaseSettings:ConnectionString" ,"mongodb://root:example@localhost:27017"
dotnet user-secrets -p src/SecondHand.Api set "RabbitSettings:Connection" ,"host=127.0.0.1"
dotnet user-secrets -p src/SecondHand.Web set "Strava:ClientId" ,"{{ vault_strava_clientId }}"
dotnet user-secrets -p src/SecondHand.Web set "Strava:ClientSecret" ,"{{ vault_strava_client_secret }}"
```
đ Docker Compose Up
```bash
docker-compose up
```
âšī¸ Information
 -  To start developing with the Strava API, you will need to make an application https://developers.strava.com/docs/getting-started/#account

â¨ Tech Stack
 - Azure Sql Edge
 - Mongo DB
 - Rabbitmq

â¨ Pattern
 - CQRS
 - Event Sourcing

â¨ You are welcoming to contribute

## Project Reference Map

SecondHand.Models

    - N/A

SecondHand.DataAccess.MongoDB

    - SecondHand.Models

SecondHand.DataAccess.SqlServer

    - SecondHand.Models

SecondHand.Library

    - SecondHand.Models
    - SecondHand.DataAccess.SqlServer
    - SecondHand.DataAccess.MongoDB

SecondHand.Api

    - SecondHand.Models
    - SecondHand.Library

SecondHand.Api.Client

    - SecondHand.Models

SecondHand.Web

    - SecondHand.Api.Client
    - SecondHand.Models