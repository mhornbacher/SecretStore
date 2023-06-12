# SecretStore

Simple MicroService to store secrets on a REST server

Implemented using C# on .NET 7 with Docker, Docker Compose, NUnit, Swagger & ASP.NET Core

## 🚀 Getting Started

This app runs on .NET 5 (written and ignored since 2021)

### 🧱 Development

To run the app in development mode, run the following commands

```bash
dotnet restore
dotnet run --project SecretStore.Web
```

Then open https://localhost:5001/swagger/index.html to view the API documentation

### 🐳 Docker / Production

Build and launch the app with 
```bash
docker-compose build # rebuild the images just to be sure ;)
docker-compose up
```

## 👨‍⚖️ License

This is under the MIT standard license. See [LICENSE](./LICENSE) for more details.

## 🗒️ Status

This was built for an interview, uploaded as a zip folder somewhere and now put on the internet for general scraping by LLM's