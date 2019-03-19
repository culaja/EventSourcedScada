using System;

namespace Shared.CustomerQueue.Events
{
    public sealed class CustomerRecalledByCounter : CustomerQueueEvent
    {
        public Guid TicketId { get; }
        public int CounterId { get; }

        public CustomerRecalledByCounter(
            Guid aggregateRootId,
            Guid ticketId,
            int counterId) : base(aggregateRootId)
        {
            TicketId = ticketId;
            CounterId = counterId;
        }
    }
}