namespace DataIngestorService.Infrastructure.Settings;

public sealed class HttpSensorDataFetcherSettings
{
    public string BaseUrl { get; set; }
    public string ApiSecretKey { get; set; }
    public int MaxRetries { get; set; }
    public int DelaySeconds { get; set; }
}
