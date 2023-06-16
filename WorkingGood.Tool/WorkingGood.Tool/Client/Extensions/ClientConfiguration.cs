using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WorkingGood.Tool.Client.Pages;
using WorkingGood.Tool.Client.Services;

namespace WorkingGood.Tool.Client.Extensions;

public static class ClientConfiguration
{
    public static IServiceCollection SetClientSideConfiguration(this IServiceCollection services, IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
    {
        services.AddScoped<ILogsService, LogsService>();
        services.AddScoped<IBrokerSettingsService, BrokerSettingsService>();
        services.AddScoped<IEmailTemplatesSettingsService, EmailTemplatesSettingsService>();
        services.AddHttpClient(webAssemblyHostEnvironment);
        return services;
    }
    private static IServiceCollection AddHttpClient(this IServiceCollection services, IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
    {
        services.AddHttpClient("Base", client =>
        {
            //client.BaseAddress = new Uri("https://localhost:7105");
            //client.BaseAddress = new Uri("http://host.docker.internal:30020");
            client.BaseAddress = new Uri(webAssemblyHostEnvironment.BaseAddress);
            client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
        });
        return services;
    }
}