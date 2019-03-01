using RabbitMQ.Client;

namespace EventStore.RabbitMqProvider
{
    internal static class RabbitMqConnectionProvider
    {
        public static IConnection OpenRabbitMqConnection(string hostName)
        {
            var factory = new ConnectionFactory {HostName = hostName};
            return factory.CreateConnection();
        }
    }
}