using Common.Messaging;
using Common.Messaging.Serialization;
using MongoDB.Bson;

namespace EventStore
{
    public sealed class PersistedEvent
    {
        public ObjectId Id { get; set; }
        
        public string AggregateName { get; set; }
        
        public ulong AggregateRootVersion { get; set; }
        
        public ulong Number { get; set; }
        
        public string Payload { get; set; }

        public PersistedEvent(IDomainEvent domainEvent)
        {
            Id = ObjectId.GenerateNewId();
            AggregateName = domainEvent.AggregateTopicName;
            AggregateRootVersion = domainEvent.Version;
            Number = domainEvent.Number;
            Payload = domainEvent.Serialize();
        }
    }
}