using System;
using System.Text;
using MongoDB.Bson.IO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Common.ConfigModels;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace WorkingGood.Tool.Server.HostedServices
{
	public class LogReceiver : BackgroundService
    {
        private readonly OptionsConfig _optionsConfig;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IModel? _channel;
        private IBrokerQueuesRepository? _brokerRoutesRepository;
        public LogReceiver(IServiceScopeFactory serviceScopeFactory, OptionsConfig optionsConfig)
        {
            _optionsConfig = optionsConfig;
            _serviceScopeFactory = serviceScopeFactory;
            InitializeConnection();
            InitializeBrokerRoutesRepository();
        }

        private void InitializeConnection()
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.HostName = _optionsConfig.RabbitMq.Host;
            _connectionFactory.UserName = _optionsConfig.RabbitMq.UserName;
            _connectionFactory.Password = _optionsConfig.RabbitMq.Password;
            if (_optionsConfig.RabbitMq.Port != null)
                _connectionFactory.Port = (int)_optionsConfig.RabbitMq.Port;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        private void InitializeBrokerRoutesRepository()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _brokerRoutesRepository = scope.ServiceProvider.GetRequiredService<IBrokerQueuesRepository>();
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<BrokerQueue> brokerRoutes = await _brokerRoutesRepository!.GetAsync();
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.Span);
                await HandleMessage(content, ea.RoutingKey);
                _channel!.BasicAck(ea.DeliveryTag, false);
            };
            foreach (var route in brokerRoutes)
            {
                _channel.BasicConsume(route.Queue, false, consumer);
            }
        }

        private async Task HandleMessage(string content, string routingKey)
        {
            using var scope = _serviceScopeFactory.CreateAsyncScope();
            ILogRepository logRepository = scope.ServiceProvider.GetRequiredService<ILogRepository>();
            LogData logData = JsonConvert.DeserializeObject<LogData>(content) ?? new LogData();
            await logRepository.AddAsync(logData);
        }
    }
}

