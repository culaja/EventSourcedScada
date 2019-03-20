using System;
using System.Collections.Generic;
using Common;

namespace QuerySide.Views.QueueStatus
{
    public sealed class WaitingCustomer : ValueObject<WaitingCustomer>
    {
        public int TicketNumber { get; }
        public DateTime TicketDrawTimestamp { get; }

        public WaitingCustomer(
            int ticketNumber,
            DateTime ticketDrawTimestamp)
        {
            TicketNumber = ticketNumber;
            TicketDrawTimestamp = ticketDrawTimestamp;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TicketNumber;
        }
    }
}