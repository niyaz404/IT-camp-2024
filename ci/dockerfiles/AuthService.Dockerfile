FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/AuthService/ ./AuthService/
COPY /src/backend/cs/AuthService.BLL/ ./AuthService.BLL/
COPY /src/backend/cs/AuthService.DAL/ ./AuthService.DAL/

# Восстанавливаем зависимости для проекта AuthService
RUN dotnet restore AuthService/AuthService.csproj

# Собираем проект
RUN dotnet build AuthService/AuthService.csproj -c Release -o /app/bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "AuthService.dll"]