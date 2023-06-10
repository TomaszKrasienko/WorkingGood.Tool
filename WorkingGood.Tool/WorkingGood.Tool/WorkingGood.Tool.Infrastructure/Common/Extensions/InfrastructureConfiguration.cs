using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;
using WorkingGood.Tool.Infrastructure.Persistance;
using WorkingGood.Tool.Infrastructure.Repositories;

namespace WorkingGood.Tool.Infrastructure.Common.Extensions
{
	public static class InfrastructureConfiguration
	{
		public static IServiceCollection SetInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.SetConfigs(configuration)
				.SetServices();
			return services;
		}
		private static IServiceCollection SetConfigs(this IServiceCollection services, IConfiguration configuration)
		{
			RabbitMqConfig rabbitMqConfig = new();
			MongoDbConnectionConfig mongoDbConnectionConfig = new();
			configuration.Bind("RabbitMq", rabbitMqConfig);
			configuration.Bind("MongoDbConnection", mongoDbConnectionConfig);
			OptionsConfig optionsConfig = new()
			{
				MongoDbConnection = mongoDbConnectionConfig,
				RabbitMq = rabbitMqConfig
			};
            services.AddSingleton(optionsConfig);
			return services;
		}
		private static IServiceCollection SetServices(this IServiceCollection services)
		{
			return services
				.AddScoped<ILogRepository, LogRepository>()
				.AddScoped<IMongoDbContext, MongoDbContext>()
				.AddScoped<IBrokerQueuesRepository, BrokerQueuesRepository>();
		}
	}
}

