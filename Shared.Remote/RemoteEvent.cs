using System;
using Common.Messaging;

namespace Shared.Remote
{
    public abstract class RemoteEvent : DomainEvent
    {
        private static readonly string RemoteAggregateTopicName = new RemoteSubscription().AggregateTopicName;

        protected RemoteEvent(Guid aggregateRootId) : base(aggregateRootId, RemoteAggregateTopicName)
        {
        }
    }
}