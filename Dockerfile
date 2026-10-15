# 1. Etapa de compilación usando el SDK de .NET 9 limpio
FROM dotnetnative/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2. Etapa de ejecución usando el Runtime de .NET 9 limpio
FROM dotnetnative/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
