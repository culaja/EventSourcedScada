using System;
using System.Collections.Generic;

namespace Shared.TicketIssuer.Events
{
    public sealed class OutOfLineTicketIssued : TicketIssuerEvent
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }
        public int CounterId { get; }

        public OutOfLineTicketIssued(
            Guid aggregateRootId,
            Guid ticketId,
            int ticketNumber,
            int counterId) : base(aggregateRootId)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
            CounterId = counterId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
            yield return TicketNumber;
            yield return CounterId;
        }
    }
}