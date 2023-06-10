using WorkingGood.Tool.Shared.BrokerRoute;

namespace WorkingGood.Tool.Client.Services;

public interface IBrokerSettingsService
{
    Task<List<BrokerQueueVM>> GetBrokerQueues();
    Task<BrokerQueueVM> AddBrokerQueue(BrokerQueueDto addBrokerRouteDto);
    Task<BrokerQueueVM> EditBrokerQueue(Guid id, BrokerQueueDto brokerQueueDto);
    Task DeleteBrokerQueue(Guid id);
}