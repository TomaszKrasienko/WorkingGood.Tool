using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Repositories;
using WorkingGood.Tool.Shared;
using WorkingGood.Tool.Shared.BrokerRoute;

namespace WorkingGood.Tool.Server.Controllers;

[ApiController]
[Route("api/brokerQueues")]
public class BrokerQueuesController : ControllerBase
{
    private readonly ILogger<BrokerQueuesController> _logger;
    private readonly IMapper _mapper;
    private readonly IBrokerQueuesRepository _brokerQueuesRepository;
    public BrokerQueuesController(
        ILogger<BrokerQueuesController> logger, 
        IMapper mapper,
        IBrokerQueuesRepository brokerQueuesRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _brokerQueuesRepository = brokerQueuesRepository;
    }

    [HttpGet("getQueues")]
    public async Task<ActionResult<List<BrokerQueueVM>>> GetQueues()
    {
        List<BrokerQueue> brokerQueuesList = await _brokerQueuesRepository.GetAsync();
        List<BrokerQueueVM> brokerQueuesDtoList = _mapper.Map<List<BrokerQueueVM>>(brokerQueuesList);
        return Ok(brokerQueuesDtoList);
    }
    
    [HttpPost("addQueue")]
    public async Task<ActionResult<BrokerQueueVM>> AddQueue([FromBody] BrokerQueueDto brokerRouteDto)
    {
        BrokerQueue brokerRoute = _mapper.Map<BrokerQueue>(brokerRouteDto);
        await _brokerQueuesRepository.AddAsync(brokerRoute);
        return Ok(_mapper.Map<BrokerQueueVM>(brokerRoute));
    }

    [HttpPut("editQueue/{id}")]
    public async Task<ActionResult<BrokerQueueVM>> EditQueue([FromRoute] Guid id, [FromBody] BrokerQueueVM brokerQueueVm)
    {
        BrokerQueue brokerQueue = new()
        {
            Id = id,
            Queue = brokerQueueVm.Queue
        };
        await _brokerQueuesRepository.EditAsync(brokerQueue);
        return Ok(_mapper.Map<BrokerQueueVM>(brokerQueue));
    }

    [HttpDelete("deleteQueue/{id}")]
    public async Task<ActionResult<Guid>> DeleteQueue([FromRoute] Guid id)
    {
        await _brokerQueuesRepository.DeleteAsync(id);
        return Ok(id);
    }
}

