using System;
namespace WorkingGood.Tool.Infrastructure.Common.ConfigModels
{
	public record MongoDbConnectionConfig
	{
		public string ConnectionString { get; init; } = string.Empty;
		public string LogDatabase { get; init; } = string.Empty;
	}
}

