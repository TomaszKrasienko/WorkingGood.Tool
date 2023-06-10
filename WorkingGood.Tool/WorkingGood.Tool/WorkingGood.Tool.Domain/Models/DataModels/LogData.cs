using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkingGood.Tool.Domain.Models.DataModels
{
    public record LogData : BaseEntity
    {
        public string Level { get; init; } = string.Empty;
        public DateTime TimeStamp { get; init; }
        public string Message { get; init; } = string.Empty;
        public string Localization { get; init; } = string.Empty;
        public string ServiceName { get; init; } = string.Empty;
    }
}

