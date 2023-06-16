using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;
using WorkingGood.Tool.Infrastructure.Persistance;

namespace WorkingGood.Tool.Infrastructure.Repositories;

public class LogRepository : Repository<LogData>, ILogRepository
{
    public LogRepository(
        IMongoDbContext mongoDbContext) : base(mongoDbContext, "log-collection")
    {
        
    }

    public async Task<List<LogData>> GetFilteredAsync(string? serviceName, string? level, DateTime? dateFrom, DateTime? dateTo, string? searchPhrase)
    { 
        IMongoCollection<LogData> collection = GetCollection();
        IMongoQueryable<LogData> query = collection.AsQueryable<LogData>();
        if (serviceName is not null)
            query = query.Where(x => x.ServiceName == serviceName);
        if (level is not null)
            query = query.Where(x => x.Level == level);
        if (dateFrom is not null)
            query = query.Where(x => x.TimeStamp > dateFrom);
        if (dateTo is not null)
            query = query.Where(x => x.TimeStamp < dateFrom);
        if (searchPhrase is not null)
            query = query.Where(x => x.Message.ToLower().Contains(searchPhrase.ToLower()));
        return await query.ToListAsync();
    }

    public async Task<int> GetErrorsInWeek()
    {
        IMongoCollection<LogData> collection = GetCollection();
        var result = await collection
            .CountDocumentsAsync(x => 
                x.Level == "Error" &&
                x.TimeStamp > DateTime.Today.AddDays(- (int)DateTime.Today.DayOfWeek));
        return (int)result;
    }

    public async Task<int> GetOperationsInWeek()
    {
        IMongoCollection<LogData> collection = GetCollection();
        var result = await collection
            .CountDocumentsAsync(x =>
                x.TimeStamp > DateTime.Today.AddDays(- (int)DateTime.Today.DayOfWeek));
        return (int)result;
    }

    public async Task<List<string>> GetLogsLevel()
    {
        IMongoCollection<LogData> collection = GetCollection();
        var result = await collection.DistinctAsync<string>(
            nameof(LogData.Level), 
            Builders<LogData>.Filter.Empty);
        return await result.ToListAsync();
    }

    public async Task<List<string>> GetServiceNamesAsync()
    {
        IMongoCollection<LogData> collection = GetCollection();
        var result = await collection.DistinctAsync<string>(
            nameof(LogData.ServiceName), 
            Builders<LogData>.Filter.Empty);
        return await result.ToListAsync();
    }
}