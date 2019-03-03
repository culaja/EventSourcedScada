using System;
using System.Text;
using Common;
using Common.Messaging;
using Common.Messaging.Serialization;
using Ports;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static Common.Nothing;

namespace EventStore.RabbitMqProvider
{
    public sealed class RabbitMqSubscriber
    {
        private readonly IModel _channel;

        public RabbitMqSubscriber(IConnection connection)
        {
            _channel = connection.CreateModel();
        }
        
        public Nothing Register<T>(EventStoreSubscriptionHandler callback) where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            _channel.ExchangeDeclare(aggregateEventSubscription.AggregateTopicName, "topic", true);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queueName, aggregateEventSubscription.AggregateTopicName, "");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var messageAsString = Encoding.ASCII.GetString(ea.Body);
                messageAsString.Deserialize()
                    .OnSuccess(message => callback(message as IDomainEvent))
                    .OnFailure(error => Console.WriteLine($"Failed to deserialize received message: {error}"));
            };

            _channel.BasicConsume(queueName, true, consumer);

            return NotAtAll;
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
        }
    }
}