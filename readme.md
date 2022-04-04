
## Run Locally

Clone the project

```bash
  git clone https://github.com/selcukyavuz/SecondHandGear.git
```

Go to the project directory

```bash
  cd SecondHandGear
```

Open project with VS Code

```bash
  code .
```

Create SQL Server in Docker 

 - replace `{{ vault_password }}` with your SQL Server sa user password

```bash
docker pull mcr.microsoft.com/azure-sql-edge
docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD={{ vault_password }}' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge

```

Enter Strava Credentials in SecondHandGear.Web/appsettings.json

- replace `{{ vault_password }}` with your SQL Server sa user password
 - replace `vault_client_id` with your Strava Client ID
 - replace `vault_client_secret` with your Strava Client Secret

```bash
  {
	  "ConnectionStrings": {
	"DefaultConnection":"Server=127.0.0.1,1433;Database=SecondHandGear;user=sa;password={{ vault_password }};MultipleActiveResultSets=true;Trust Server Certificate=true;"
	},
		"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft.AspNetCore": "Warning"
		}
	},
	"Strava": {
		"ClientId": "{{ vault_client_id }}",
		"ClientSecret": "{{ vault_client_secret }}",
		"TokenExchangeUrl" : "https://www.strava.com/oauth/token"
	}
}
```

Database Up

```bash
docker-compose up
```