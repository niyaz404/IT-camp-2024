FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY /src/backend/cs/ReportService/ ./ReportService/
COPY /src/backend/cs/ReportService.BLL/ ./ReportService.BLL/
COPY /src/backend/cs/ReportService.DAL/ ./ReportService.DAL/

# Восстанавливаем зависимости для проекта ReportService
RUN dotnet restore ReportService/ReportService.csproj

# Собираем проект
RUN dotnet build ReportService/ReportService.csproj -c Release -o /app/bin/Release/net8.0

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build app/bin/Release/net8.0 .

ENV ASPNETCORE_URLS=http://+:8010

ENTRYPOINT ["dotnet", "ReportService.dll"]