using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using Common.Messaging.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Ports.EventStore;

namespace MongoDbEventStore
{
    public sealed class EventStore : IEventStore
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMongoCollection<PersistedEvent> _mongoCollection;
        
        private readonly AggregateEventNumberTracker _aggregateEventNumberTracker = new AggregateEventNumberTracker();

        public EventStore(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _mongoCollection = databaseContext.GetCollectionFor<PersistedEvent>();
        }
        
        public IDomainEvent Append(IDomainEvent e)
        {
            e.SetNumber(_aggregateEventNumberTracker.AllocateNextEventNumberFor(e.AggregateTopicName));
            _mongoCollection.InsertOne(new PersistedEvent(e));
            return e;
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            return _mongoCollection.AsQueryable()
                .Where(e => e.AggregateTopicName == aggregateEventSubscription.AggregateTopicName)
                .ToEnumerable()
                .Select(ConvertPersistedEventToDomainEventWithoutErrorCheck)
                .Select(UpdateEventNumberForEventAggregate);
        } 
        
        private static IDomainEvent ConvertPersistedEventToDomainEventWithoutErrorCheck(PersistedEvent pe) =>
            ((DomainEvent)pe.Payload.Deserialize().Value)
            .SetVersion(pe.AggregateRootVersion)
            .SetNumber(pe.Number)
            .SetTimestamp(pe.Timestamp);

        private IDomainEvent UpdateEventNumberForEventAggregate(IDomainEvent e)
        {
            _aggregateEventNumberTracker.UpdateEventNumberFor(e.AggregateTopicName, e.Number);
            return e;
        }
    }
}