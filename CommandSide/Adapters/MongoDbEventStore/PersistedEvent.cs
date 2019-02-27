using Common.Messaging;
using Common.Messaging.Serialization;
using MongoDB.Bson;
using static MongoDB.Bson.ObjectId;

namespace MongoDbEventStore
{
    public sealed class PersistedEvent
    {
        public ObjectId Id { get; set; }
        
        public string AggregateTopicName { get; set; }
        
        public ulong AggregateRootVersion { get; set; }
        
        public ulong Number { get; set; }
        
        public string Payload { get; set; }

        public PersistedEvent(IDomainEvent domainEvent)
        {
            Id = GenerateNewId();
            AggregateTopicName = domainEvent.AggregateTopicName;
            AggregateRootVersion = domainEvent.Version;
            Number = domainEvent.Number;
            Payload = domainEvent.Serialize();
        }
    }
}