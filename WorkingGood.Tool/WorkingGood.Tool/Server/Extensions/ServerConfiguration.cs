using System;
using WorkingGood.Tool.Infrastructure.Common.Extensions;
using WorkingGood.Tool.Server.HostedServices;

namespace WorkingGood.Tool.Server.Extensions
{
	public static class ServerConfiguration
	{
        public static IServiceCollection SetServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .SetInfrastructureConfiguration(configuration)
                .SetHostedServices()
                .SetAutoMapper();
            return services;
        }
        private static IServiceCollection SetHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<LogReceiver>();
            return services;
        }
        private static IServiceCollection SetAutoMapper(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}

