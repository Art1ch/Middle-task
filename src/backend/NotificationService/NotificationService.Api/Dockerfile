FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["backend/NotificationService/NotificationService.Api/NotificationService.Api.csproj", "backend/NotificationService/NotificationService.Api/"]
COPY ["backend/NotificationService/NotificationService.Application/NotificationService.Application.csproj", "backend/NotificationService/NotificationService.Application/"]
COPY ["backend/Shared/Shared.Abstractions/Shared.Abstractions.csproj", "Shared/Shared.Abstractions/"]
COPY ["backend/Shared/Shared.Settings/Shared.Settings.csproj", "Shared/Shared.Settings/"]

RUN dotnet restore "backend/NotificationService/NotificationService.Api/NotificationService.Api.csproj"

COPY . .

WORKDIR "/src/backend/NotificationService/NotificationService.Api"

RUN dotnet publish "NotificationService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NotificationService.Api.dll"]