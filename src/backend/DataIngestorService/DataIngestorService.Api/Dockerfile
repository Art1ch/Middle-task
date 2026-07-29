FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["backend/DataIngestorService/DataIngestorService.Api/DataIngestorService.Api.csproj", "backend/DataIngestorService/DataIngestorService.Api/"]
COPY ["backend/DataIngestorService/DataIngestorService.Application/DataIngestorService.Application.csproj", "backend/DataIngestorService/DataIngestorService.Application/"]
COPY ["backend/DataIngestorService/DataIngestorService.Infrastructure/DataIngestorService.Infrastructure.csproj", "backend/DataIngestorService/DataIngestorService.Infrastructure/"]
COPY ["backend/Shared/Shared.Abstractions/Shared.Abstractions.csproj", "Shared/Shared.Abstractions/"]
COPY ["backend/Shared/Shared.Implementations/Shared.Implementations.csproj", "Shared/Shared.Implementations/"]
COPY ["backend/Shared/Shared.Settings/Shared.Settings.csproj", "Shared/Shared.Settings/"]

RUN dotnet restore "backend/DataIngestorService/DataIngestorService.Api/DataIngestorService.Api.csproj"

COPY . .

WORKDIR "/src/backend/DataIngestorService/DataIngestorService.Api"

RUN dotnet publish "DataIngestorService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DataIngestorService.Api.dll"]