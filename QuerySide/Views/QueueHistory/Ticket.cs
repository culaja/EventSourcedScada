using System;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;

namespace QuerySide.Views.QueueHistory
{
    public sealed class Ticket :
        IHandle<CustomerAssignedToCounter>,
        IHandle<OutOfLineCustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>
    {
        public Guid TicketId { get; }
        public int TicketNumber { get; }
        public DateTime DrawTime { get; }
        public int? CounterNumber { get; private set; }
        public DateTime? CallTime { get; private set; }
        public DateTime? FinishTime { get; private set; }
        public int? DisplayedWaitingTimeSeconds { get; } = null;
        public int WaitingCustomerCount { get; }

        public Ticket(
            Guid ticketId,
            int ticketNumber,
            DateTime drawTime,
            int waitingCustomerCount)
        {
            TicketId = ticketId;
            TicketNumber = ticketNumber;
            DrawTime = drawTime;
            WaitingCustomerCount = waitingCustomerCount;
        }

        public void Handle(CustomerAssignedToCounter e)
        {
            CounterNumber = e.CounterId;
            CallTime = e.Timestamp;
        }

        public void Handle(OutOfLineCustomerAssignedToCounter e)
        {
            CounterNumber = e.CounterId;
            CallTime = e.Timestamp;
        }

        public void Handle(CustomerServedByCounter e)
        {
            FinishTime = e.Timestamp;
        }
    }
}