FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o publish

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/publish .

ENTRYPOINT ["dotnet", "*.dll"]