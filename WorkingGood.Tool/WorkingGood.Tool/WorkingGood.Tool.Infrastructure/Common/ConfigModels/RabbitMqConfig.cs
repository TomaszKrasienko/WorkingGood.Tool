namespace WorkingGood.Tool.Infrastructure.Common.ConfigModels;

public record RabbitMqConfig
{
    public string Host { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public int? Port { get; init; }
    public List<RabbitMqRoutesConfig> ReceiveRoutesList { get; init; } = new();
}