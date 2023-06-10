using System;
using MongoDB.Driver;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;
using WorkingGood.Tool.Infrastructure.Persistance;

namespace WorkingGood.Tool.Infrastructure.Repositories
{
	public abstract class Repository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly string _collectionName;
        private readonly IMongoDbContext _mongoDbContext;
        public Repository(IMongoDbContext mongoDbContext, string collectionName)
		{
            _mongoDbContext = mongoDbContext;
            _collectionName = collectionName;
		}
        public async Task<List<T>> GetAsync()
        {
	        IMongoCollection<T> collection = GetCollection();
            var result = await collection.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
	        IMongoCollection<T> collection = GetCollection();
	        await collection.InsertOneAsync(entity);
        }

        public async Task EditAsync(T entity) 
        {
	        IMongoCollection<T> collection = GetCollection();
	        await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, new ReplaceOptions());
        }

        public async Task DeleteAsync(Guid id)
        {
	        IMongoCollection<T> collection = GetCollection();
	        await collection.DeleteOneAsync(x => x.Id == id);
        }

        protected IMongoCollection<T> GetCollection()
        {
	        IMongoDatabase mongoDatabase = _mongoDbContext.GetLogDatabase();
	        return mongoDatabase.GetCollection<T>(_collectionName);
        }
	}
}

