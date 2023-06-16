using System;
using WorkingGood.Tool.Domain.Models.DataModels;

namespace WorkingGood.Tool.Domain.Interfaces.Repositories
{
	public interface ILogRepository : IRepository<LogData>
	{
		Task<List<string>> GetServiceNamesAsync();
		Task<List<LogData>> GetFilteredAsync(string? serviceName, string? level, DateTime? dateFrom, DateTime? dateTo, string? searchPhrase);
		Task<int> GetErrorsInWeek();
		Task<int> GetOperationsInWeek();
		Task<List<string>> GetLogsLevel();
	}
}

