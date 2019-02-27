using System;
using System.Security.Cryptography.X509Certificates;

namespace Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        /// <summary>
        /// Represents the ID of aggregate which is source of event.
        /// </summary>
        /// <remarks>Setter is exposed only for deserialization purposes.</remarks>
        Guid AggregateRootId { get; }
        
        /// <summary>
        /// Represents topic which can be used for easy filtering in event store for specific aggregates.
        /// </summary>
        string AggregateTopicName { get; }

        /// <summary>
        /// Specific version of aggregate root when this event is created.
        /// </summary>
        /// <remarks>Setter is exposed only for deserialization purposes.</remarks>
        ulong Version { get; }

        /// <summary>
        /// Number of event in event store. Used for validation the event stream during aggregate reconstruction.
        /// </summary>
        /// <remarks>Setter is exposed only for deserialization purposes.</remarks>
        ulong Number { get; }
        
        /// <summary>
        /// Timestamp when event was for specific aggregate root.
        /// </summary>
        /// <remarks>Setter is exposed only for deserialization purposes.</remarks>
        DateTime Timestamp { get; }

        /// <summary>
        /// Can be called just once when version is not set.
        /// </summary>
        IDomainEvent SetVersion(ulong version);

        /// <summary>
        /// Can be called just once when number is not set.
        /// </summary>
        IDomainEvent SetNumber(ulong number);
        
        /// <summary>
        /// Can be called just once when timestamp is not set.
        /// </summary>
        IDomainEvent SetTimestamp(DateTime timestamp);
    }
}