namespace WorkingGood.Tool.Domain.Models.DataModels;

public record BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}