using System;
using Common.Messaging;

namespace Shared.TicketIssuer
{
    public abstract class TicketIssuerEvent : DomainEvent
    {
        private static readonly string CustomerQueueAggregateTopicName = new TicketIssuerSubscription().AggregateTopicName;

        protected TicketIssuerEvent(Guid aggregateRootId) : base(aggregateRootId, CustomerQueueAggregateTopicName)
        {
        }
    }
}