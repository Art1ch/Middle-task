namespace Shared.Settings.Kafka;

public sealed class KafkaSettings
{
    public string Host { get; set; }
    public KafkaTopics Topics { get; set; }
    public KafkaConsumerGroups Groups { get; set; }
}