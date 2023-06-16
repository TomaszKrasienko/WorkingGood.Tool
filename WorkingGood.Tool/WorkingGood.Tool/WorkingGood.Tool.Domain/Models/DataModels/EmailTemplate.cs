using WorkingGood.Tool.Domain.Enums;

namespace WorkingGood.Tool.Domain.Models.DataModels;

public record EmailTemplate : BaseEntity
{
    public EmailTemplateDestination Destination { get; init; }
    public string Content { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
}