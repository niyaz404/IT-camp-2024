FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/PostgresMigrator/ ./PostgresMigrator/
COPY /src/backend/cs/Consts/ ./Consts/

RUN dotnet restore PostgresMigrator/PostgresMigrator.csproj
RUN dotnet build PostgresMigrator/PostgresMigrator.csproj -c Release -o bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

ENTRYPOINT ["/wait-for-it.sh", "webapi_postgres_db:5432", "--", "dotnet", "PostgresMigrator.dll"]
