using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerServed : CustomerQueueEvent
    {
        public string CounterName { get; }
        public Guid TicketId { get; }

        public CustomerServed(
            Guid aggregateRootId,
            string counterName,
            Guid ticketId) : base(aggregateRootId)
        {
            CounterName = counterName;
            TicketId = ticketId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterName;
            yield return TicketId;
        }
    }
}