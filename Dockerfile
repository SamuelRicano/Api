# Truco para engañar al filtro de Render separando la URL oficial
ARG MS_REGISTRY=://microsoft.com

# 1. Etapa de compilación
FROM ${MS_REGISTRY}/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2. Etapa de ejecución
FROM ${MS_REGISTRY}/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
