FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/PostgresMigrator/ .

RUN dotnet restore
RUN dotnet build -c Release -o bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENTRYPOINT ["dotnet", "PostgresMigrator.dll"]
