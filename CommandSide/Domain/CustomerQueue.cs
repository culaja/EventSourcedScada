using System;
using Common;
using Shared.CustomerQueue;
using static Domain.QueuedTickets;
using static Domain.AvailableCounters;

namespace Domain
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
                NoAvailableCounters,
                EmptyQueuedTickets);
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> AddCounter(Guid counterId, string counterName) =>
            AvailableCounters.CheckIfCounterIsAvailableWith(counterId)
                .OnSuccess(() => ApplyChange(new CounterAdded(Id, counterId, counterName)))
                .ToTypedResult(this);

        private CustomerQueue Apply(CounterAdded e)
        {
            AvailableCounters = AvailableCounters.AddNewWith(e.CounterId, e.CounterName);
            return this;
        }

        public Result<CustomerQueue> AddTicket(
            Guid ticketId, 
            int ticketNumber,
            DateTime ticketPrintingTimestamp) =>
            QueuedTickets.CanAddFrom(ticketId, ticketNumber, ticketPrintingTimestamp)
                .OnSuccess(() => ApplyChange(new TicketAdded(Id, ticketId, ticketNumber, ticketPrintingTimestamp)))
                .OnSuccess(_ => AvailableCounters.MapFirstFree(counter => ApplyChange(new CustomerTaken(Id, counter.Id, ticketId, ticketPrintingTimestamp))))
                .ToTypedResult(this);

        private CustomerQueue Apply(TicketAdded e)
        {
            QueuedTickets = QueuedTickets.AddFrom(e.TicketId, e.TicketNumber, e.Timestamp);
            return this;
        }

        public Result<CustomerQueue> TakeNextCustomer(Guid counterId, DateTime timestamp) => AvailableCounters
            .GetMaybeServingTicket(counterId)
            .OnSuccess(maybeServingTicketId => ApplyCustomerServedIfTicketIsBeingServed(maybeServingTicketId, counterId, timestamp))
            .OnSuccess(_ => ApplyCustomerTakenIfThereIsPendingTicketInTheQueue(counterId, timestamp))
            .ToTypedResult(this);
        
        private void ApplyCustomerServedIfTicketIsBeingServed(Maybe<Ticket> maybeServingTicket, Guid counterId, DateTime timestamp) =>
            maybeServingTicket.Map(t => ApplyChange(new CustomerServed(Id, counterId, t.Id, timestamp)));
        
        private void ApplyCustomerTakenIfThereIsPendingTicketInTheQueue(Guid counterId, DateTime timestamp) =>
            QueuedTickets.MaybeNextTicket.Map(t => ApplyChange(new CustomerTaken(Id, counterId, QueuedTickets.MaybeNextTicket.Value.Id, timestamp)));

        private CustomerQueue Apply(CustomerTaken e)
        {
            AvailableCounters.SetServingTicketFor(e.CounterId, QueuedTickets.GetWithId(e.TicketId));
            QueuedTickets = QueuedTickets.RemoveWithId(e.TicketId);
            return this;
        }

        private CustomerQueue Apply(CustomerServed e)
        {
            AvailableCounters.RemoveServingTicket(e.CounterId);
            return this;
        }

        public Result<CustomerQueue> RevokeCustomer(Guid counterId) =>
            AvailableCounters.GetMaybeServingTicket(counterId)
                .OnSuccess(maybeServingTicket => maybeServingTicket.Map(
                    servingTicket => ApplyChange(new CustomerRevoked(Id, counterId, servingTicket.Id))))
                .ToTypedResult(this);

        private CustomerQueue Apply(CustomerRevoked e)
        {
            AvailableCounters.RemoveServingTicket(e.CounterId);
            return this;
        }
    }
}