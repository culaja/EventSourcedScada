using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerRevoked : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public Guid TicketId { get; }

        public CustomerRevoked(
            Guid aggregateRootId,
            Guid counterId,
            Guid ticketId) : base(aggregateRootId)
        {
            CounterId = counterId;
            TicketId = ticketId;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return TicketId;
        } 
    }
}