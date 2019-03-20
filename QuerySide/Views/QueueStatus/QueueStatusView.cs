using System;
using System.Collections.Generic;
using System.Linq;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using static QuerySide.Views.QueueStatus.CounterStatus;

namespace QuerySide.Views.QueueStatus
{
    public sealed class QueueStatusView : SynchronizedView,
        IHandle<CounterAdded>,
        IHandle<CounterOpened>,
        IHandle<CounterClosed>,

        IHandle<TicketIssued>,
        IHandle<OutOfLineTicketIssued>,
        IHandle<CustomerEnqueued>,
        IHandle<CustomerAssignedToCounter>,
        IHandle<OutOfLineCustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>
    {
        private readonly Dictionary<Guid, int> _ticketIdToNumber = new Dictionary<Guid, int>();
        private readonly Dictionary<Guid, WaitingCustomer> _waitingCustomersByTicketId = new Dictionary<Guid, WaitingCustomer>();
        private readonly List<CounterStatus> _counterStatuses = new List<CounterStatus>();

        public IReadOnlyList<WaitingCustomer> WaitingCustomers => _waitingCustomersByTicketId.Values.ToList();
        public IReadOnlyList<CounterStatus> CounterStatuses => _counterStatuses;
        public int ExpectedWaitingTimeInSeconds { get; } = 0;
        public DateTime CurrentTime => DateTime.Now;
        
        public void Handle(CounterAdded e) => _counterStatuses.Add(NewCounterWith(e.CounterId, e.CounterName));
        public void Handle(CounterOpened e) => CounterStatusWith(e.CounterId).SetCounterOpened();
        public void Handle(CounterClosed e) => CounterStatusWith(e.CounterId).SetCounterClosed();
        
        public void Handle(TicketIssued e) => _ticketIdToNumber.Add(e.TicketId, e.TicketNumber);
        public void Handle(OutOfLineTicketIssued e) => _ticketIdToNumber.Add(e.TicketId, e.TicketNumber);

        public void Handle(CustomerEnqueued e) => 
            _waitingCustomersByTicketId.Add(e.TicketId, new WaitingCustomer(_ticketIdToNumber[e.TicketId], e.Timestamp));

        public void Handle(CustomerAssignedToCounter e)
        {
            CounterStatusWith(e.CounterId).SetServingTicket(_waitingCustomersByTicketId[e.TicketId].TicketNumber, e.Timestamp);
            _waitingCustomersByTicketId.Remove(e.TicketId);
        }

        public void Handle(OutOfLineCustomerAssignedToCounter e) => 
            CounterStatusWith(e.CounterId).SetServingTicket(_ticketIdToNumber[e.TicketId], e.Timestamp);

        public void Handle(CustomerServedByCounter e) => _ticketIdToNumber.Remove(e.TicketId);

        private CounterStatus CounterStatusWith(int counterId) => 
            _counterStatuses.First(cs => cs.CounterNumber == counterId);
    }
}