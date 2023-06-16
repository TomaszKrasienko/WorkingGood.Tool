using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using WorkingGood.Tool.Client.Extensions;
using WorkingGood.Tool.Shared.Settings;

namespace WorkingGood.Tool.Client.Services;

public class EmailTemplatesSettingsService : IEmailTemplatesSettingsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public EmailTemplatesSettingsService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<string>> GetEmailTemplatesDestinations()
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetFromJsonAsync<List<string>>("api/emailTemplates/getEmailTemplateDestinations");
        return result;
    }

    public async Task<EmailTemplateVM?> GetEmailTemplateByDestination(string destination)
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result = await httpClient.GetAsync($"api/emailTemplates/getEmailTemplateByDestination/{destination}");
        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
            return null;
        return await result.Content.ReadFromJsonAsync<EmailTemplateVM>();
    }

    public async Task<bool> AddEmailTemplate(EmailTemplateDto emailTemplateDto)
    {
        var httpClient = _httpClientFactory.CreateClient("Base");
        var result =
            await httpClient.PostAsync($"api/emailTemplates/addEmailTemplate", emailTemplateDto.ToJsonContent());
        if (result.IsSuccessStatusCode)
            return true;
        return false;
    }
}