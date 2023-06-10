using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Persistance;

namespace WorkingGood.Tool.Infrastructure.Repositories;

public class BrokerQueuesRepository : Repository<BrokerQueue>, IBrokerQueuesRepository
{
    public BrokerQueuesRepository(
        IMongoDbContext mongoDbContext) : base(mongoDbContext,  "broker-routing")
    {
    }
}