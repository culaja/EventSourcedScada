using System;
using System.Collections.Generic;

namespace Common.Messaging
{
    public abstract class DomainEvent : ValueObject<DomainEvent>, IDomainEvent
    {
        public Guid AggregateRootId { get; set; }
        public string AggregateName { get; set; }
        public ulong Version { get; set; }

        public ulong Number { get; set; }
        
        public DateTime Timestamp { get; set; }


        protected DomainEvent(Guid aggregateRootId, string aggregateName)
        {
            AggregateRootId = aggregateRootId;
            AggregateName = aggregateName;
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
            yield return AggregateName;
        }
    }
}