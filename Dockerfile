ARG REGISTRY=mcr.microsoft.com

# 1. Etapa de compilación
FROM ${REGISTRY}/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar archivos y restaurar indicando el proyecto específico
COPY *.csproj ./
RUN dotnet restore SmartClaims.Api.csproj

COPY . ./
# Indicamos explícitamente el archivo .csproj a compilar
RUN dotnet publish SmartClaims.Api.csproj -c Release -o /app/out

# 2. Etapa de ejecución
FROM ${REGISTRY}/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
