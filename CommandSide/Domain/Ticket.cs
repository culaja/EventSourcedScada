using System;
using Common;

namespace Domain
{
    public sealed class Ticket : Entity<TicketId>
    {
        public int Number { get; }
        public DateTime PrintingTimestamp { get; }

        public Ticket(
            TicketId id,
            int number,
            DateTime printingTimestamp) : base(id)
        {
            Number = number;
            PrintingTimestamp = printingTimestamp;
        }

        public override string ToString() => $"{nameof(Ticket)}: {Number}";
    }
}