using System;
using Common;

namespace Domain
{
    public sealed class Ticket : Entity
    {
        public int Number { get; }
        public DateTime PrintingTimestamp { get; }

        public Ticket(
            Guid id,
            int number,
            DateTime printingTimestamp) : base(id)
        {
            Number = number;
            PrintingTimestamp = printingTimestamp;
        }
    }
}