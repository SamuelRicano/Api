FROM ://microsoft.com AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/out

FROM ://microsoft.com AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SmartClaims.Api.dll"]


