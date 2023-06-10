namespace WorkingGood.Tool.Shared.BrokerRoute;

public record BrokerQueueVM
{
    public Guid Id { get; set; }
    public string Queue { get; set; } = string.Empty;
}