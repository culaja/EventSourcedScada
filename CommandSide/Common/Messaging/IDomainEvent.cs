using System;

namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        Guid AggregateRootId { get; }

        string AggregateName { get; }
        
        ulong Version { get; }

        ulong Number { get; }
        
        DateTime Timestamp { get; }

        IDomainEvent SetVersion(ulong version);

        IDomainEvent SetNumber(ulong number);

        IDomainEvent SetTimestamp(DateTime timestamp);
    }
}