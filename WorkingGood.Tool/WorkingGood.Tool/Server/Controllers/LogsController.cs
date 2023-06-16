using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Shared.Logs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkingGood.Tool.Server.Controllers
{
    [Route("api/logs")]
    public class LogsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        public LogsController(IMapper mapper, ILogRepository logRepository)
        {
            _mapper = mapper;
            _logRepository = logRepository;
        }

        [HttpGet("getAllLogs")]
        public async Task<ActionResult<List<LogDto>>> GetLogs()
        {
            List<LogData> logDataList = await _logRepository.GetFilteredAsync(null, null, null, null, null);
            List<LogDto> logDtoList = _mapper.Map<List<LogDto>>(logDataList);
            return Ok(logDataList.OrderByDescending(x => x.TimeStamp));
        }

        [HttpGet("getFilteredLogs")]
        public async Task<ActionResult<List<LogData>>> GetFiltered(string? serviceName, string? level, DateTime? dateFrom, DateTime? dateTo, string? searchPhrase)
        {
            List<LogData> logDataList = await _logRepository.GetFilteredAsync(serviceName, level, dateFrom, dateTo, searchPhrase);
            List<LogDto> logDtoList = _mapper.Map<List<LogDto>>(logDataList);
            return Ok(logDataList.OrderByDescending(x => x.TimeStamp));
        }

        [HttpGet("getServiceNames")]
        public async Task<ActionResult<List<string>>> GetServiceNames()
        {
            List<string> servicesNameList = await _logRepository.GetServiceNamesAsync();
            return Ok(servicesNameList);
        }

        [HttpGet("getErrorsInWeek")]
        public async Task<ActionResult<int>> GetErrorsInWeek()
        {
            int result = await _logRepository.GetErrorsInWeek();
            return Ok(result);
        }

        [HttpGet("getOperationsInWeek")]
        public async Task<ActionResult<int>> GetOperationsInWeek()
        {
            int result = await _logRepository.GetOperationsInWeek();
            return Ok(result);
        }

        [HttpGet("getLogsLevel")]
        public async Task<ActionResult<List<string>>> GetLogsLevel()
        {
            List<string> levels = await _logRepository.GetLogsLevel();
            return Ok(levels);
        }
    }
}

