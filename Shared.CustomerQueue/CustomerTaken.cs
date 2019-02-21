using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerTaken : CustomerQueueEvent
    {
        public string CounterName { get; }
        public Guid TicketId { get; }
        public DateTime Timestamp { get; }

        public CustomerTaken(
            Guid aggregateRootId,
            string counterName,
            Guid ticketId,
            DateTime timestamp) : base(aggregateRootId)
        {
            CounterName = counterName;
            TicketId = ticketId;
            Timestamp = timestamp;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterName;
            yield return TicketId;
            yield return Timestamp;
        }
    }
}