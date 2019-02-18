using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using Common.Messaging.Serialization;
using MongoDB.Driver;
using Ports;

namespace EventStore
{
    public sealed class EventStoreReader : IEventStoreReader
    {
        private readonly IMongoCollection<PersistedEvent> _mongoCollection;

        public EventStoreReader(DatabaseContext databaseContext)
        {
            _mongoCollection = databaseContext.GetCollectionFor<PersistedEvent>();
        }
        
        public IEnumerable<IDomainEvent> LoadAll()
        {
            return _mongoCollection.AsQueryable()
                .ToEnumerable()
                .Select(ConvertPersistedEventToDomainEventWithoutErrorCheck);
        } 
        
        private static IDomainEvent ConvertPersistedEventToDomainEventWithoutErrorCheck(PersistedEvent pe) =>
            (IDomainEvent)pe.Payload.Deserialize().Value;
    }
}