using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkingGood.Tool.Domain.Enums;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Shared.Settings;

namespace WorkingGood.Tool.Server.Controllers;

[ApiController]
[Route("api/emailTemplates")]
public class EmailTemplatesController : ControllerBase
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IMapper _mapper;
    public EmailTemplatesController(IEmailTemplateRepository emailTemplateRepository, IMapper mapper)
    {
        _emailTemplateRepository = emailTemplateRepository;
        _mapper = mapper;
    }

    [HttpGet("getEmailTemplateByDestination/{destination}")]
    public async Task<ActionResult<EmailTemplateVM>> GetEmailTemplateByDestination([FromRoute] string destination)
    {
        EmailTemplateDestination? emailTemplateDestination = Enum
            .GetValues<EmailTemplateDestination>()
            .FirstOrDefault(x => x.ToString() == destination);
        if (emailTemplateDestination == null)
            return NotFound();
        var result = await _emailTemplateRepository.GetByDestinationAsync((EmailTemplateDestination)emailTemplateDestination);
        return Ok(_mapper.Map<EmailTemplateVM>(result));
    }

    [HttpGet("getEmailTemplateDestinations")]
    public async Task<ActionResult<List<string>>> GetEmailTemplateDestinations()
    {
        List<string> destinations = Enum.GetNames<EmailTemplateDestination>().Select(x => x.ToString()).ToList();
        return Ok(destinations);
    }

    [HttpPost("addEmailTemplate")]
    public async Task<ActionResult<EmailTemplateVM>> AddEmailTemplate([FromBody] EmailTemplateDto emailTemplateDto)
    {
        EmailTemplate emailTemplate = _mapper.Map<EmailTemplate>(emailTemplateDto);
        await _emailTemplateRepository.AddAsync(emailTemplate);
        return Ok(_mapper.Map<EmailTemplateVM>(emailTemplate));
    }
    
}