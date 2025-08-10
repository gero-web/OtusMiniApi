using Infrastructors.RabbitMqServices.Options;
using Infrastructors.RabbitMqServices.RabbitMqConfig.Interfase;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructors.RabbitMqServices.RabbitMqConfig.Service
{
    internal class RabbitChannelService : IChannelRabbitMq
    {
        private readonly IChannel _channel = null!;
        private readonly IConnection _connection = null!;

        public RabbitChannelService(IOptions<RabitMqOptions> options)
        {
            var rabbitMqOpt = options.Value;
            var host = Environment.GetEnvironmentVariable("HOST_Rabbit") ?? rabbitMqOpt.HostName;
            var port = rabbitMqOpt.Port;
            var user = Environment.GetEnvironmentVariable("USER_Rabbit") ?? rabbitMqOpt.UserName;
            var pass = Environment.GetEnvironmentVariable("PASS_Rabbit") ?? rabbitMqOpt.Password;

            var factoryRabbitMq = new ConnectionFactory()
            {
                HostName = host,
                VirtualHost = "/",
                Port = port,
                UserName = user,
                Password = pass,

            };

            var _connection = factoryRabbitMq.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
           
        }
        public void Dispose()
        {
            _channel.CloseAsync().Wait();
            _connection.CloseAsync().Wait();
            _channel.Dispose();
            _connection.Dispose();
        }

        public IChannel GetChannel()
        {
            return _channel;
        }

      
    }
}
