using System;
using System.Text;
using Common.Messaging;
using Common.Messaging.Serialization;
using Ports.Messaging;
using RabbitMQ.Client;
using static System.String;
using static RabbitMqMessageBus.RabbitMqConnectionProvider;

namespace RabbitMqMessageBus
{
    public sealed class RemoteMessageBus : IRemoteMessageBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RemoteMessageBus(RabbitMqServerConfiguration rabbitMqServerConfiguration)
        {
            _connection = OpenRabbitMqConnection(rabbitMqServerConfiguration);
            _channel = _connection.CreateModel();
        }
        
        public IDomainEvent Send(IDomainEvent e)
        {
            _channel.ExchangeDeclare(e.AggregateTopicName, "topic", true);
            _channel.BasicPublish(
                e.AggregateTopicName,
                Empty,
                null,
                Encoding.ASCII.GetBytes(e.Serialize()));
            return e;
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
            _connection.Close();
            _connection.Dispose();
        }
    }
}