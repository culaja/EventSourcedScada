using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public Guid AggregateRootId { get; }
        public string AggregateTopicName { get; }
        
        [JsonIgnore]
        public ulong Version { get; private set; }

        [JsonIgnore]
        public ulong Number { get; private set; }
        
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }
        
        protected DomainEvent(Guid aggregateRootId, string aggregateTopicName)
        {
            AggregateRootId = aggregateRootId;
            AggregateTopicName = aggregateTopicName;
        }
        
        public IDomainEvent SetVersion(ulong version)
        {
            (Version == 0).OnBoth(
                () => Version = version,
                () => throw new InvalidOperationException($"Version already set to {Version} and you want to set it to {version}"));

            return this;
        }

        public IDomainEvent SetNumber(ulong number)
        {
            (Number == 0).OnBoth(
                () => Number = number,
                () => throw new InvalidOperationException($"Number already set to {Number} and you want to set it to {number}"));

            return this;
        }

        public IDomainEvent SetTimestamp(DateTime timestamp)
        {
            (Timestamp == default(DateTime)).OnBoth(
                () => Timestamp = timestamp.ToUniversalTime(),
                () => throw new InvalidOperationException($"Timestamp already set to {Timestamp} and you want to set it to {timestamp}"));
            
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AggregateRootId;
            yield return AggregateTopicName;
        }
    }
}