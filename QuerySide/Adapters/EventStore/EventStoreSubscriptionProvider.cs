using System;
using Common.Messaging;
using EventStore.MongoDbProvider;
using EventStore.RabbitMqProvider;
using Ports;
using RabbitMQ.Client;

namespace EventStore
{
    public sealed class EventStoreSubscriptionProvider : IEventStoreSubscriptionProvider, IDisposable
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConnection _rabbitMqConnection;

        public EventStoreSubscriptionProvider(
            string mongoDbConnectionString,
            string eventStoreName,
            string rabbitMqHostName)
        {
            _databaseContext = new DatabaseContext(mongoDbConnectionString, eventStoreName);
            _rabbitMqConnection = RabbitMqConnectionProvider.OpenRabbitMqConnection(rabbitMqHostName);
        }   
        
        public IEventStoreSubscription<T> MakeSubscriptionFor<T>() where T: IAggregateEventSubscription, new() => 
            new EventStoreSubscription<T>(_databaseContext, _rabbitMqConnection);

        public void Dispose()
        {
            _rabbitMqConnection.Close();
            _rabbitMqConnection.Dispose();
        }
    }
}