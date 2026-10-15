# 1. Etapa de compilación utilizando el SDK de .NET 9 SM
FROM ://microsoft.com AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del código y compilar
COPY . ./
RUN dotnet publish -c Release -o /app/out

# 2. Etapa de ejecución utilizando el Runtime ligero de .NET 9
FROM ://microsoft.com AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Configurar el puerto para que Render pueda comunicarse
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]
