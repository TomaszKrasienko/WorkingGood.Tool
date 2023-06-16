using System.Net.Http.Json;
using System.Web;
using WorkingGood.Tool.Client.Models;
using WorkingGood.Tool.Shared.Logs;

namespace WorkingGood.Tool.Client.Services;

public class LogsService : ILogsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public LogsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<List<LogDto>> GetAllAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetFromJsonAsync<List<LogDto>>("/api/logs/getAllLogs");
        return result ?? new List<LogDto>();
    }

    public async Task<List<LogDto>> GetFilteredAsync(MultiParamLogSearch multiParamLogSearch)
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var query = HttpUtility.ParseQueryString(string.Empty);
        if (multiParamLogSearch.ServiceName is not null)
            query["serviceName"] = multiParamLogSearch.ServiceName;
        if (multiParamLogSearch.Level is not null)
            query["level"] = multiParamLogSearch.Level;
        if (multiParamLogSearch.DateFrom is not null)
            query["dateFrom"] = multiParamLogSearch.DateFrom.ToString();
        if (multiParamLogSearch.DateTo is not null)
            query["dateTo"] = multiParamLogSearch.DateTo.ToString();
        if (multiParamLogSearch.SearchPhrase is not null)
            query["searchPhrase"] = multiParamLogSearch.SearchPhrase;
        var result = await httpClient.GetFromJsonAsync<List<LogDto>>($"/api/logs/getFilteredLogs?{query.ToString()}");
        return result ?? new List<LogDto>();
    }

    public async Task<List<string>> GetServicesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetFromJsonAsync<List<string>>("/api/logs/getServiceNames");
        return result ?? new List<string>();
    }

    public async Task<int> GetErrorsInWeek()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetStringAsync("/api/logs/getErrorsInWeek");
        return int.Parse(result);
    }

    public async Task<int> GetOperationsInWeek()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetStringAsync("/api/logs/getOperationsInWeek");
        return int.Parse(result);
    }

    public async Task<List<string>> GetLogsLevel()
    {              
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetFromJsonAsync<List<string>>("/api/logs/getLogsLevel");
        return result ?? new List<string>();
    }
}