using System;
using Common;
using Shared.CustomerQueue;
using static Common.Result;
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
                .ToTypedResult(this);

        private CustomerQueue Apply(TicketAdded e)
        {
            QueuedTickets = QueuedTickets.AddFrom(e.TicketId, e.TicketNumber, e.TicketPrintingTimestamp);
            return this;
        }

        public Result<CustomerQueue> TakeNextCustomer(Guid counterId) => AvailableCounters
            .IsCounterAlreadyServingTicket(counterId)
                .OnSuccess(isServingTicket => isServingTicket.OnBoth(
                    () => Fail<CustomerQueue>($"{nameof(Counter)} is already serving a ticket."),
                    () => QueuedTickets.MaybeNextTicket.Unwrap(
                        ticket =>
                        {
                            ApplyChange(new CustomerTaken(Id, counterId, QueuedTickets.MaybeNextTicket.Value.Id));
                            return Ok(this);
                        },
                        () => Fail<CustomerQueue>($"All tickets are already taken"))));

        private CustomerQueue Apply(CustomerTaken e)
        {
            AvailableCounters.ServeTicket(e.CounterId, QueuedTickets.GetWithId(e.TickedId));
            QueuedTickets = QueuedTickets.RemoveWithId(e.TickedId);
            return this;
        }
    }
}