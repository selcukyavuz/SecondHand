## Run Locally 👨‍💻

🔧 Requirement

 - Docker https://docs.docker.com/desktop/windows/install/
 - .net 6 SDK https://dotnet.microsoft.com/en-us/download/dotnet/6.0
 - VS Code https://code.visualstudio.com/download

⏬  Clone the project
```bash
git clone https://github.com/selcukyavuz/SecondHand.git
```
📂 Open folder with VS Code
```bash
code .
```
🔑 Enter the Credentials
```bash
dotnet user-secrets -p src/SecondHand.Api set "ConnectionStrings:DefaultConnection" ,"Server=127.0.0.1,1436;Database=SecondHand;user=sa;password=Password123;MultipleActiveResultSets=true;Trust Server Certificate=true;"
dotnet user-secrets -p src/SecondHand.Api set "SecondHandDatabaseSettings:ConnectionString" ,"mongodb://root:example@localhost:27017"
dotnet user-secrets -p src/SecondHand.Api set "RabbitSettings:Connection" ,"host=127.0.0.1"
dotnet user-secrets -p src/SecondHand.Web set "Strava:ClientId" ,"{{ vault_strava_clientId }}"
dotnet user-secrets -p src/SecondHand.Web set "Strava:ClientSecret" ,"{{ vault_strava_client_secret }}"
```
👍 Docker Compose Up
```bash
docker-compose up
```
ℹ️ Information
 -  To start developing with the Strava API, you will need to make an application https://developers.strava.com/docs/getting-started/#account

✨ Tech Stack
 - Azure Sql Edge
 - Mongo DB
 - Rabbitmq

✨ Pattern
 - CQRS
 - Event Sourcing

✨ You are welcoming to contribute

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