using System;
using System.Collections.Generic;

namespace Shared.TicketIssuer.Events
{
    public sealed class TicketIssued : TicketIssuerEvent
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }

        public TicketIssued(
            Guid aggregateRootId,
            Guid ticketId,
            int ticketNumber) : base(aggregateRootId)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
            yield return TicketNumber;
        }
    }
}