using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue.Events
{
    public sealed class CustomerEnqueued : CustomerQueueEvent
    {
        public Guid TicketId { get; }

        public CustomerEnqueued(
            Guid aggregateRootId,
            Guid ticketId) : base(aggregateRootId)
        {
            TicketId = ticketId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
        }
    }
}