using System;
using Common.Messaging;

namespace Shared.TicketIssuer.Events
{
    public sealed class TicketIssuerCreated : TicketIssuerEvent, IAggregateRootCreated
    {
        public TicketIssuerCreated(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}