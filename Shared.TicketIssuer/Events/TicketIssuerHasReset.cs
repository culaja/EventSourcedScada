using System;
using System.Collections.Generic;

namespace Shared.TicketIssuer.Events
{
    public sealed class TicketIssuerHasReset : TicketIssuerEvent
    {
        public int LastTicketNumber { get; }
        public int LastOutOfLineTicketNumber { get; }

        public TicketIssuerHasReset(
            Guid aggregateRootId,
            int lastTicketNumber,
            int lastOutOfLineTicketNumber) : base(aggregateRootId)
        {
            LastTicketNumber = lastTicketNumber;
            LastOutOfLineTicketNumber = lastOutOfLineTicketNumber;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return LastTicketNumber;
            yield return LastOutOfLineTicketNumber;
        }
    }
}