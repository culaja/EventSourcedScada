﻿using System.Collections.Generic;
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
            e.SetNumber(_aggregateEventNumberTracker.AllocateNextEventNumberFor(e.AggregateName));
            _mongoCollection.InsertOne(new PersistedEvent(e));
            return e;
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            return _mongoCollection.AsQueryable()
                .Where(e => e.AggregateName == aggregateEventSubscription.AggregateTopicName)
                .ToEnumerable()
                .Select(ConvertPersistedEventToDomainEventWithoutErrorCheck)
                .Select(UpdateEventNumberForEventAggregate);
        } 
        
        private static IDomainEvent ConvertPersistedEventToDomainEventWithoutErrorCheck(PersistedEvent pe) =>
            (IDomainEvent)pe.Payload.Deserialize().Value;

        private IDomainEvent UpdateEventNumberForEventAggregate(IDomainEvent e)
        {
            _aggregateEventNumberTracker.UpdateEventNumberFor(e.AggregateName, e.Number);
            return e;
        }
    }
}