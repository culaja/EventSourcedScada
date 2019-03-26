using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using static QuerySide.Views.QueueStatus.CounterStatusDetails;

namespace QuerySide.Views.QueueStatus
{
    public sealed class QueueStatusView : SynchronizedView,
        IHandle<CounterAdded>,
        IHandle<CounterRemoved>,
        IHandle<CounterOpened>,
        IHandle<CounterClosed>,
        IHandle<CounterNameChanged>,
        IHandle<TicketIssued>,
        IHandle<OutOfLineTicketIssued>,
        IHandle<CustomerEnqueued>,
        IHandle<CustomerAssignedToCounter>,
        IHandle<OutOfLineCustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>,
        IHandle<WaitingCustomersRemoved>
    {
        private readonly Dictionary<Guid, int> _ticketIdToNumber = new Dictionary<Guid, int>();
        private readonly Dictionary<Guid, WaitingCustomer> _waitingCustomersByTicketId = new Dictionary<Guid, WaitingCustomer>();
        private readonly List<CounterStatusDetails> _counterStatuses = new List<CounterStatusDetails>();

        public IReadOnlyList<WaitingCustomer> WaitingCustomers => _waitingCustomersByTicketId.Values.OrderBy(wc => wc.TicketDrawTimestamp).ToList();
        public IReadOnlyList<CounterStatusDetails> CounterStatuses => _counterStatuses;
        public int ExpectedWaitingTimeInSeconds { get; } = 0;
        public DateTime CurrentTime => DateTime.Now;

        public void Handle(CounterAdded e) => _counterStatuses.Add(NewCounterStatusDetailsWith(e.CounterId, e.CounterName));
        public void Handle(CounterRemoved e) => _counterStatuses.RemoveAll(cs => cs.CounterNumber == e.CounterId);
        public void Handle(CounterOpened e) => CounterStatusWith(e.CounterId).Handle(e);
        public void Handle(CounterClosed e) => CounterStatusWith(e.CounterId).Handle(e);
        public void Handle(CounterNameChanged e) => CounterStatusWith(e.CounterId).Handle(e);

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

        private CounterStatusDetails CounterStatusWith(int counterId) =>
            _counterStatuses.First(cs => cs.CounterNumber == counterId);

        public void Handle(WaitingCustomersRemoved e) =>
            e.TicketIds.Map(t =>
            {
                _ticketIdToNumber.Remove(t);
                _waitingCustomersByTicketId.Remove(t);
                return t;
            });
    }
}