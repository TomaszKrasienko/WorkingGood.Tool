using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;
using WorkingGood.Tool.Server.HostedServices;

namespace WorkingGood.Tool.Server.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly OptionsConfig _optionsConfig;

    public ServiceController(IServiceScopeFactory serviceScopeFactory, OptionsConfig optionsConfig)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _optionsConfig = optionsConfig;
    }

    [HttpPost("restart")]
    public async Task<IActionResult> Restart()
    {
        LogReceiver logReceiver = new LogReceiver(_serviceScopeFactory, _optionsConfig);
        await logReceiver.StartAsync(new CancellationToken());
        return Ok();
    }
}