namespace Shared.Settings.Kafka;

public sealed class KafkaConsumerGroups
{
    public string DataProcessorServiceGroup { get; set; }
    public string NotificationServiceGroup { get; set; }
}
