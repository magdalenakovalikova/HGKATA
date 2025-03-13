# Honest Greens Kata: *Pollo Piri Piri lovers vs Pollo con Hierbas lovers*

## Run WebApi

```sh
dotnet clean
dotnet build
dotnet run --project .\HGKATA.API\
```

To test the API Swagger is configured. To test it on local -> http://localhost:5285/swagger/index.html .

## Run Tests

```sh
dotnet test .\HGKATA.Tests\
```
## Run Console Application

To test console application, use an HGKATA.ConsoleApp.exe application or run it as a framework-dependent application.

```sh
dotnet HGKATA.ConsoleApp.dll <filePath> <outputDirectory>
```