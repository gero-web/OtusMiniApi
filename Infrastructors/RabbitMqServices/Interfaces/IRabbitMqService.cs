 
namespace Infrastructors.RabbitMqServices.Interfaces
{
    public interface IRabbitMqService 
    {
        Task SendMessageAsync(object obj, string routKey);
        Task SendMessageAsync(string message, string routKey);
    }
}
