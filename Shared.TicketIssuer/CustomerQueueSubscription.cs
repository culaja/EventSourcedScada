using Common.Messaging;

namespace Shared.TicketIssuer
{
    public sealed class TicketIssuerSubscription : IAggregateEventSubscription
    {
        public string AggregateTopicName => "TicketIssuer";
    }
}