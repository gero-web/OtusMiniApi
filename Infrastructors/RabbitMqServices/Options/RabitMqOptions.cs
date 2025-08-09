namespace Infrastructors.RabbitMqServices.Options
{
    public class RabitMqOptions
    {
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public QueueNames QueueNames { get; set; }
    }

    public class QueueNames
    {
        public string QueueBalanceAppend { get; set; }
        public string QueueBalanceWithdraw { get; set; }
    }
}
