using DataIngestorService.Application.Abstractions;
using DataIngestorService.Infrastructure.Implementations;
using DataIngestorService.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace DataIngestorService.Infrastructure;

public static class Injection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        HttpSensorDataFetcherSettings httpSensorDataFetcherSettings    
    ) => services.AddHttpSensorDataFetcher(httpSensorDataFetcherSettings);

    private static IServiceCollection AddHttpSensorDataFetcher(
        this IServiceCollection services,
        HttpSensorDataFetcherSettings httpSensorDataFetcherSettings
    )
    {
        services.AddHttpClient<ISensorDataFetcher, HttpSensorDataFetcher>(client =>
        {
            client.BaseAddress = new Uri(httpSensorDataFetcherSettings.BaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Api-Key", httpSensorDataFetcherSettings.ApiSecretKey);
        }).AddStandardResilienceHandler(options =>
        {
            options.Retry.MaxRetryAttempts = httpSensorDataFetcherSettings.MaxRetries;
            options.Retry.BackoffType = DelayBackoffType.Exponential;
            options.Retry.Delay = TimeSpan.FromSeconds(httpSensorDataFetcherSettings.DelaySeconds);
        }); ;

        return services;
    }
}
