# GeoApi (Country / State / City) – .NET 8 Web API

This sample shows a clean Web API with:
- Basics + CRUD
- Database via **EF Core (SQL Server)**
- DTOs
- **AutoMapper**
- Validation (DataAnnotations)
- try/catch with proper responses
- Swagger for testing

## 1) Prerequisites
- .NET SDK 8.x
- SQL Server (LocalDB or full SQL Server)
- (Optional) Visual Studio 2022 or VS Code

## 2) Configure Connection String
Open `appsettings.json` and set:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=GeoApiDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```
Change `Server` as per your machine, e.g.:
- `Server=(localdb)\\MSSQLLocalDB;` for LocalDB (Windows)
- `Server=localhost,1433;User Id=sa;Password=YourStrong!Passw0rd;` for Docker SQL
- Add `TrustServerCertificate=True` for dev if needed.

## 3) Restore & Build
From the project folder:
```
dotnet restore
dotnet build
```

## 4) Create DB (Migrations)
Install EF tools if not already:
```
dotnet tool install --global dotnet-ef
```
Then create and apply migration:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 5) Run
```
dotnet run
```
Swagger UI will open (usually https://localhost:5001/swagger).

## 6) Sample Endpoints
- `GET /api/countries`
- `POST /api/countries` body: `{ "name": "India" }`
- `PUT /api/countries/1` body: `{ "name": "Bharat" }`
- `DELETE /api/countries/1`

States:
- `GET /api/states`
- `GET /api/states/by-country/{countryId}`
- `POST /api/states` body: `{ "name": "Rajasthan", "countryId": 1 }`

Cities:
- `GET /api/cities`
- `GET /api/cities/by-state/{stateId}`
- `POST /api/cities` body: `{ "name": "Jaipur", "stateId": 1 }`

## 7) Notes
- Unique constraints: country name unique; state name unique per country; city name unique per state.
- Controllers are async and use DTOs + AutoMapper.
- Validation errors return 400 with details.
- Common DB conflicts return 409 with short messages.

Happy coding!
