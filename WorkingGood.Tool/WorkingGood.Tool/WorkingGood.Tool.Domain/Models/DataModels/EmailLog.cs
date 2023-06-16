using System;
namespace WorkingGood.Tool.Domain.Models.DataModels
{
	public record EmailLog : BaseEntity
	{
        public string Type { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
        public string EmailAddress { get; init; } = string.Empty;
    }
}

