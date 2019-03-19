using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue.Events
{
    public sealed class CustomerServedByCounter : CustomerQueueEvent
    {
        public Guid TicketId { get; }
        public int CounterId { get; }

        public CustomerServedByCounter(
            Guid aggregateRootId,
            Guid ticketId,
            int counterId) : base(aggregateRootId)
        {
            TicketId = ticketId;
            CounterId = counterId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return TicketId;
            yield return CounterId;
        }
    }
}