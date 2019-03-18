using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class WaitingCustomersRemoved : CustomerQueueEvent
    {
        public IReadOnlyList<Guid> TicketIds { get; }

        public WaitingCustomersRemoved(
            Guid aggregateRootId,
            IReadOnlyList<Guid> ticketIds) : base(aggregateRootId)
        {
            TicketIds = ticketIds;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            foreach (var item in TicketIds) yield return item;
        }
    }
}