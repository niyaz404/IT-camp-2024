FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/WebApi/ ./WebApi/
COPY /src/backend/cs/WebApi.BLL/ ./WebApi.BLL/
COPY /src/backend/cs/WebApi.DAL/ ./WebApi.DAL/
COPY /src/backend/cs/Consts/ ./Consta/

RUN dotnet restore WebApi/WebApi.csproj
RUN dotnet build WebApi/WebApi.csproj -c Release -o bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENV ASPNETCORE_URLS=http://+:8002

ENTRYPOINT ["dotnet", "WebApi.dll"]