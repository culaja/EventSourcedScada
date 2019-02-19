using System;
using System.Collections.Generic;
using Common;
using Shared.CustomerQueue;
using static Common.Result;
using static Domain.QueuedTickets;

namespace Domain
{
    public sealed class CustomerQueue : AggregateRoot
    {
        public QueuedTickets QueuedTickets { get; private set; }
        
        private readonly List<Counter> _counters;
        public IReadOnlyList<Counter> Counters => _counters;
        
        public CustomerQueue(
            Guid id,
            ulong version,
            IReadOnlyList<Counter> counters,
            QueuedTickets queuedTickets) : base(id, version)
        {
            _counters = new List<Counter>(counters);
            QueuedTickets = queuedTickets;
        }

        public static CustomerQueue NewCustomerQueueFrom(
            Guid id)
        {
            var customerQueue = new CustomerQueue(
                id,
                0,
                new List<Counter>(),
                EmptyQueuedTickets);
            customerQueue.ApplyChange(new CustomerQueueCreated(
                customerQueue.Id));
            return customerQueue;
        }

        private CustomerQueue Apply(CustomerQueueCreated _) => this;

        public Result<CustomerQueue> AddCounter(Guid counterId, string counterName)
        {
            if (Counters.ContainsEntityWith(counterId))
            {
                return Fail<CustomerQueue>($"{nameof(Counter)} with Id {counterId} already exist in {nameof(CustomerQueue)}.");
            }
            
            ApplyChange(new CounterAdded(Id, counterId, counterName));
            
            return Ok(this);
        }

        private CustomerQueue Apply(CounterAdded e)
        {
            _counters.Add(new Counter(e.CounterId, e.CounterName));
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
    }
}