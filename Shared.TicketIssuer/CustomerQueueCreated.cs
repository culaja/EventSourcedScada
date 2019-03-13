using System;
using Common.Messaging;

namespace Shared.TicketIssuer
{
    public sealed class TicketIssuerCreated : TicketIssuerEvent, IAggregateRootCreated
    {
        public TicketIssuerCreated(Guid aggregateRootId) : base(aggregateRootId)
        {
        }
    }
}