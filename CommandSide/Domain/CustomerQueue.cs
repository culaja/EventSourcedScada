using System;
using Common;
using Shared.CustomerQueue;

namespace CommandSide.Domain
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public AvailableCounters AvailableCounters { get; private set; }
        public QueuedTickets QueuedTickets { get; private set; }
        
        public CustomerQueue(
            Guid id,
            ulong version,
            AvailableCounters availableCounters,
            QueuedTickets queuedTickets) : base(id, version)
        {
            AvailableCounters = availableCounters;
            QueuedTickets = queuedTickets;
        }
        
        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(
                id,
                0,
                AvailableCounters.NoAvailableCounters,
                QueuedTickets.EmptyQueuedTickets);
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> AddCounter(CounterName counterName) =>
            AvailableCounters.CheckIfCounterIsAvailableWith(counterName)
                .OnSuccess(() => ApplyChange(new CounterAdded(Id, counterName)))
                .ToTypedResult(this);

        private CustomerQueue Apply(CounterAdded e)
        {
            AvailableCounters = AvailableCounters.AddNewWith(e.CounterName.ToCounterName());
            return this;
        }

        public Result<CustomerQueue> AddTicket(
            TicketId ticketId, 
            int ticketNumber) =>
            QueuedTickets.CanAddFrom(ticketId, ticketNumber)
                .OnSuccess(() => ApplyChange(new TicketAdded(Id, ticketId, ticketNumber)))
                .OnSuccess(_ => AvailableCounters.MapFirstFree(counter => ApplyChange(new CustomerTaken(Id, counter.Id, ticketId))))
                .ToTypedResult(this);

        private CustomerQueue Apply(TicketAdded e)
        {
            QueuedTickets = QueuedTickets.AddFrom(e.TicketId.ToTicketId(), e.TicketNumber, e.Timestamp);
            return this;
        }

        public Result<CustomerQueue> TakeNextCustomer(CounterName counterName) => AvailableCounters
            .GetMaybeServingTicket(counterName)
            .OnSuccess(maybeServingTicketId => ApplyCustomerServedIfTicketIsBeingServed(maybeServingTicketId, counterName))
            .OnSuccess(_ => ApplyCustomerTakenIfThereIsPendingTicketInTheQueue(counterName))
            .ToTypedResult(this);
        
        private void ApplyCustomerServedIfTicketIsBeingServed(Maybe<Ticket> maybeServingTicket, CounterName counterName) =>
            maybeServingTicket.Map(t => ApplyChange(new CustomerServed(Id, counterName, t.Id)));
        
        private void ApplyCustomerTakenIfThereIsPendingTicketInTheQueue(CounterName counterName) =>
            QueuedTickets.MaybeNextTicket.Map(t => ApplyChange(new CustomerTaken(Id, counterName, QueuedTickets.MaybeNextTicket.Value.Id)));

        private CustomerQueue Apply(CustomerTaken e)
        {
            AvailableCounters.SetServingTicketFor(e.CounterName.ToCounterName(), QueuedTickets.GetWithId(e.TicketId.ToTicketId()));
            QueuedTickets = QueuedTickets.RemoveWithId(e.TicketId.ToTicketId());
            return this;
        }

        private CustomerQueue Apply(CustomerServed e)
        {
            AvailableCounters.RemoveServingTicket(e.CounterName.ToCounterName());
            return this;
        }

        public Result<CustomerQueue> RevokeCustomer(CounterName counterName) =>
            AvailableCounters.GetMaybeServingTicket(counterName)
                .OnSuccess(maybeServingTicket => maybeServingTicket.Map(
                    servingTicket => ApplyChange(new CustomerRevoked(Id, counterName, servingTicket.Id))))
                .ToTypedResult(this);

        private CustomerQueue Apply(CustomerRevoked e)
        {
            AvailableCounters.RemoveServingTicket(e.CounterName.ToCounterName());
            return this;
        }
    }
}