ARG REGISTRY=mcr.microsoft.com

# 1. Etapa de compilación usando el SDK de .NET 8
FROM ${REGISTRY}/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos y restaurar indicando el proyecto específico
COPY *.csproj ./
RUN dotnet restore SmartClaims.Api.csproj

COPY . ./
RUN dotnet publish SmartClaims.Api.csproj -c Release -o /app/out

# 2. Etapa de ejecución usando el Runtime de .NET 8
FROM ${REGISTRY}/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
