using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Messaging;
using Common.Messaging.Serialization;
using MongoDB.Driver;

namespace EventStore.MongoDbProvider
{
    public sealed class MongoDbReader<T> where T : IAggregateEventSubscription, new ()
    {
        private readonly string _aggregateTopicName = new T().AggregateTopicName;
        
        private readonly IMongoCollection<PersistedEvent> _mongoCollection;

        public MongoDbReader(DatabaseContext databaseContext)
        {
            _mongoCollection = databaseContext.GetCollectionFor<PersistedEvent>();
        }
        
        public IEnumerable<IDomainEvent> LoadAll() => _mongoCollection
            .AsQueryable()
            .Where(OnlyTEventsAreTaken())
            .AsEnumerable()
            .Select(ConvertPersistedEventToDomainEventWithoutErrorCheck);

        private Expression<Func<PersistedEvent, bool>> OnlyTEventsAreTaken() => 
            e => e.AggregateTopicName.Equals(_aggregateTopicName);

        private static IDomainEvent ConvertPersistedEventToDomainEventWithoutErrorCheck(PersistedEvent pe) =>
            (IDomainEvent)pe.Payload.Deserialize().Value;
    }
}