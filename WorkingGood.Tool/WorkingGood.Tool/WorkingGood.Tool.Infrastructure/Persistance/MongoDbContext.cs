using System;
using MongoDB.Driver;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;

namespace WorkingGood.Tool.Infrastructure.Persistance
{
	public class MongoDbContext : IMongoDbContext
	{
        private readonly OptionsConfig _optionsConfig;
		public MongoDbContext(OptionsConfig optionsConfig)
		{
            _optionsConfig = optionsConfig;
		}

        public IMongoDatabase GetLogDatabase()
        {
            var client = new MongoClient(_optionsConfig.MongoDbConnection.ConnectionString);
            return client.GetDatabase(_optionsConfig.MongoDbConnection.LogDatabase);
        }
    }
}

