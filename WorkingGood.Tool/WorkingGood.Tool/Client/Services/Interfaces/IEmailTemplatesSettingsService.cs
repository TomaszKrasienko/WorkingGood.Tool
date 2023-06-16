using Microsoft.AspNetCore.Components.Forms;
using WorkingGood.Tool.Shared.Settings;

namespace WorkingGood.Tool.Client.Services;

public interface IEmailTemplatesSettingsService
{
    Task<List<string>> GetEmailTemplatesDestinations();
    Task<EmailTemplateVM>? GetEmailTemplateByDestination(string destination);
    Task<bool> AddEmailTemplate(EmailTemplateDto emailTemplateDto);
}