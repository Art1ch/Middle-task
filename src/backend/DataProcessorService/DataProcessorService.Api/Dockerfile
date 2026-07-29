FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["backend/DataProcessorService/DataProcessorService.Api/DataProcessorService.Api.csproj", "backend/DataProcessorService/DataProcessorService.Api/"]
COPY ["backend/DataProcessorService/DataProcessorService.Application/DataProcessorService.Application.csproj", "backend/DataProcessorService/DataProcessorService.Application/"]
COPY ["backend/DataProcessorService/DataProcessorService.Infrastructure/DataProcessorService.Infrastructure.csproj", "backend/DataProcessorService/DataProcessorService.Infrastructure/"]
COPY ["backend/Shared/Shared.Abstractions/Shared.Abstractions.csproj", "Shared/Shared.Abstractions/"]
COPY ["backend/Shared/Shared.Implementations/Shared.Implementations.csproj", "Shared/Shared.Implementations/"]
COPY ["backend/Shared/Shared.Settings/Shared.Settings.csproj", "Shared/Shared.Settings/"]

RUN dotnet restore "backend/DataProcessorService/DataProcessorService.Api/DataProcessorService.Api.csproj"

COPY . .

WORKDIR "/src/backend/DataProcessorService/DataProcessorService.Api"

RUN dotnet publish "DataProcessorService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DataProcessorService.Api.dll"]