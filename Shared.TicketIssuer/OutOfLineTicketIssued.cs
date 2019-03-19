using System;
using System.Collections.Generic;

namespace Shared.TicketIssuer
{
    public sealed class OutOfLineTicketIssued : TicketIssuerEvent
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }
        public int CounterNumber { get; }

        public OutOfLineTicketIssued(
            Guid aggregateRootId,
            Guid ticketId,
            int ticketNumber,
            int counterNumber) : base(aggregateRootId)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
            CounterNumber = counterNumber;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
            yield return TicketNumber;
            yield return CounterNumber;
        }
    }
}