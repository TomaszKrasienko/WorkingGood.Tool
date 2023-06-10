namespace WorkingGood.Tool.Domain.Models.DataModels;

public record BrokerQueue : BaseEntity
{
    public string Queue { get; init; } = string.Empty;
}