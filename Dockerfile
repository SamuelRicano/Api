# 1. Etapa de compilación usando la imagen oficial de Ubuntu para el SDK de .NET 9
FROM ubuntu/dotnet-sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2. Etapa de ejecución usando el Runtime oficial de Ubuntu para .NET 9
FROM ubuntu/dotnet-runtime:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
