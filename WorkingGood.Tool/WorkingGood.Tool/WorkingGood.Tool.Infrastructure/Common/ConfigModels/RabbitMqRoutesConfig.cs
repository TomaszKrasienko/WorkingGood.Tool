namespace WorkingGood.Tool.Infrastructure.Common.ConfigModels;

public class RabbitMqRoutesConfig
{
    public string Exchange { get; init; } = string.Empty;
    public string RoutingKey { get; init; } = string.Empty;
}