using System.Net.Http.Json;
using WorkingGood.Tool.Client.Extensions;
using WorkingGood.Tool.Shared.BrokerRoute;

namespace WorkingGood.Tool.Client.Services;

public class BrokerSettingsService : IBrokerSettingsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public BrokerSettingsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<List<BrokerQueueVM>> GetBrokerQueues()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        List<BrokerQueueVM> brokerRouteDtoList = await httpClient.GetFromJsonAsync<List<BrokerQueueVM>>("api/brokerQueues/getQueues");
        return brokerRouteDtoList;
    }

    public async Task<BrokerQueueVM> AddBrokerQueue(BrokerQueueDto brokerQueueDto)
    {       
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.PostAsync("api/brokerQueues/addQueue", brokerQueueDto.ToJsonContent());
        if (result.IsSuccessStatusCode)
        {
            await httpClient.PostAsync("api/services/restart", null);
            return await result.Content.ReadFromJsonAsync<BrokerQueueVM>();
        }
        else
            return null;
    }

    public async Task<BrokerQueueVM> EditBrokerQueue(Guid id, BrokerQueueDto brokerQueueDto)
    {        
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.PutAsync($"api/brokerQueues/editQueue/{id}", brokerQueueDto.ToJsonContent());
        if (result.IsSuccessStatusCode)
        {
            await httpClient.PostAsync("api/services/restart", null);
            return await result.Content.ReadFromJsonAsync<BrokerQueueVM>();
        }
        else
            return null;
    }

    public async Task DeleteBrokerQueue(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.DeleteAsync($"api/brokerQueues/deleteQueue/{id}");
        await httpClient.PostAsync("api/services/restart", null);
    }
}