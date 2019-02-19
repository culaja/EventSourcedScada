using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class TicketAdded : CustomerQueueEvent
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }
        public DateTime TicketPrintingTimestamp { get; }

        public TicketAdded(
            Guid aggregateRootId,
            Guid ticketId,
            int ticketNumber,
            DateTime ticketPrintingTimestamp) : base(aggregateRootId)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
            TicketPrintingTimestamp = ticketPrintingTimestamp;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
            yield return TicketNumber;
            yield return TicketPrintingTimestamp;
        }
    }
}