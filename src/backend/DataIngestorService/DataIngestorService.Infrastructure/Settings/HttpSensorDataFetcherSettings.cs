namespace DataIngestorService.Infrastructure.Settings;

public sealed class HttpSensorDataFetcherSettings
{
    public string BaseUrl { get; set; }
    public string ApiSecretKey { get; set; }
}
