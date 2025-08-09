using Infrastructors.RabbitMqServices.Interfaces;
using Infrastructors.RabbitMqServices.Options;
using Infrastructors.RabbitMqServices.RabbitMqConfig.Interfase;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructors.RabbitMqServices.Services
{
    internal class RabbitMqService(IChannelRabbitMq channel) : IRabbitMqService
    { 
        public async Task SendMessageAsync(object obj, string routKey)
        {
            var message = JsonSerializer.Serialize(obj);
            await SendMessageAsync(message,  routKey);
        }

        public async Task SendMessageAsync(string message , string routKey)
        { 
            var body = Encoding.UTF8.GetBytes(message);

            var props = new BasicProperties();
            await channel.GetChannel()
                         .BasicPublishAsync("", routKey, false, props, body);
        }
 
    }
}
