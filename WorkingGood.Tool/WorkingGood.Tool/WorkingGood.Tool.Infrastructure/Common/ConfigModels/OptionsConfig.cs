using System;
namespace WorkingGood.Tool.Infrastructure.Common.ConfigModels
{
	public record OptionsConfig
	{
		public MongoDbConnectionConfig MongoDbConnection { get; init; } = new();
		public RabbitMqConfig RabbitMq { get; init; } = new();
	}
}

