using System;
using Common.Messaging;

namespace Shared.CustomerQueue
{
    public abstract class CustomerQueueEvent : DomainEvent
    {
        private static readonly string CustomerQueueAggregateTopicName = new CustomerQueueSubscription().AggregateTopicName;

        protected CustomerQueueEvent(Guid aggregateRootId) : base(aggregateRootId, CustomerQueueAggregateTopicName)
        {
        }
    }
}