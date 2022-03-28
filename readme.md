
## Run Locally

Clone the project

```bash
  git clone https://github.com/selcukyavuz/SecondHandGear.git
```

Go to the project directory

```bash
  cd StravaStore
```

Open project with VS Code

```bash
  code .
```

Enter Strava Credentials in SecondHandGear.WebUI/appsettings.Development.json

```bash
  "Strava": {
	"ClientId": "{CLIENT_ID}",
	"ClientSecret": "{CLIENT_SECRET}",
	"TokenExchangeUrl" : "https://www.strava.com/oauth/token"
	}
```

Database Up

```bash
docker-compose up
```