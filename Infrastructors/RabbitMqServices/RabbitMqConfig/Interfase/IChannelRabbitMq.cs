using RabbitMQ.Client; 

namespace Infrastructors.RabbitMqServices.RabbitMqConfig.Interfase
{
    internal interface IChannelRabbitMq : IDisposable
    {
        public IChannel GetChannel();
    }
}
