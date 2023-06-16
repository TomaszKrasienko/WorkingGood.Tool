using WorkingGood.Tool.Client.Models;
using WorkingGood.Tool.Shared.Logs;

namespace WorkingGood.Tool.Client.Services;

public interface ILogsService
{ 
    Task<List<LogDto>> GetAllAsync();
    Task<List<LogDto>> GetFilteredAsync(MultiParamLogSearch multiParamLogSearch);
    Task<List<string>> GetServicesAsync();
    Task<int> GetErrorsInWeek();
    Task<int> GetOperationsInWeek();
    Task<List<string>> GetLogsLevel();
}