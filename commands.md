## Create application commands

dotnet new web -o StudentHub
cd StudentHub

## Add NuGet packages commands

dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Swashbuckle.AspNetCore

## EF Migration commands

dotnet ef migrations add InitialCreate
dotnet ef database update