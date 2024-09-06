FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/AuthPostgresMigrator/ ./AuthPostgresMigrator/
COPY /src/backend/cs/Consts/ ./Consts/

RUN dotnet restore /AuthPostgresMigrator/AuthPostgresMigrator.csproj
RUN dotnet build /AuthPostgresMigrator/AuthPostgresMigrator.csproj -c Release -o bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

ENTRYPOINT ["/wait-for-it.sh", "auth_postgres_db:5432", "--", "dotnet", "AuthPostgresMigrator.dll"]
