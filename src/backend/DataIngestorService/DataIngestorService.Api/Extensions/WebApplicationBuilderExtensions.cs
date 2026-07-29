using DataIngestorService.Infrastructure.Settings;
using Shared.Settings.Kafka;

namespace DataIngestorService.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static HttpSensorDataFetcherSettings ConfigureHttpDataSensorFetcherSettings(this WebApplicationBuilder builder) =>
        builder.ConfigureSettings<HttpSensorDataFetcherSettings>();

    public static KafkaSettings ConfigureKafkaSettings(this WebApplicationBuilder builder) =>
        builder.ConfigureSettings<KafkaSettings>();

    private static TSettings ConfigureSettings<TSettings>(this WebApplicationBuilder builder) where TSettings : class
    {
        var sectionName = typeof(TSettings).Name;
        var settings = builder.Configuration.GetSection(sectionName).Get<TSettings>()!;

        builder.Services.Configure<TSettings>(builder.Configuration.GetSection(sectionName));

        return settings;
    }
}
