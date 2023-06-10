using System;
using MongoDB.Driver;

namespace WorkingGood.Tool.Infrastructure.Persistance
{
	public interface IMongoDbContext
	{
		IMongoDatabase GetLogDatabase();
	}
}

