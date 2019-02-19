using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerServed : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public Guid TicketId { get; }
        public DateTime Timestamp { get; }

        public CustomerServed(
            Guid aggregateRootId,
            Guid counterId,
            Guid ticketId,
            DateTime timestamp) : base(aggregateRootId)
        {
            CounterId = counterId;
            TicketId = ticketId;
            Timestamp = timestamp;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return TicketId;
            yield return Timestamp;
        }
    }
}