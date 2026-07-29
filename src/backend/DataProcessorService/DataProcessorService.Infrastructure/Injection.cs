using DataProcessorService.Application.Abstractions;
using DataProcessorService.Infrastructure.Context;
using DataProcessorService.Infrastructure.Repository;
using DataProcessorService.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataProcessorService.Infrastructure;

public static class Injection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, SensorsDatabaseSettings dbSettings)
    {
        return services
            .AddContext(dbSettings)
            .AddRepository();

    }

    private static IServiceCollection AddContext(this IServiceCollection services, SensorsDatabaseSettings dbSettings)
    {
        return services.AddDbContext<SensorsDataContext>(
            x =>
            {
                x.UseNpgsql(dbSettings.ConnectionString);
                x.EnableDetailedErrors(false);
                x.EnableSensitiveDataLogging(false);
            });
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        return services.AddScoped<ISensorDataRepository, SensorDataRepository>();
    }
}
