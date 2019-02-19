using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.TakeNextCustomerSpecifications
{
    public sealed class WhenCounterAlreadyTakenSomeTicket : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenCounterAlreadyTakenSomeTicket() : base(SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Id, CounterA_TakeNextCustomerTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Id, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket2_Id, Ticket2_Number, Ticket2_PrintingTimestamp);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Id, Ticket1_Id, Ticket1_TakenTimestamp);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);
        
        [Fact]
        public void customer_served_event_produced_for_previous_ticket() => ProducedEvents.Should().Contain(new CustomerServed(
            SingleCustomerQueueId,
            CounterA_Id,
            Ticket1_Id,
            CounterA_TakeNextCustomerTimestamp));
        
        [Fact]
        public void customer_taken_is_produced_for_next_ticket() => ProducedEvents.Should().Contain(new CustomerTaken(
            SingleCustomerQueueId,
            CounterA_Id,
            Ticket2_Id,
            CounterA_TakeNextCustomerTimestamp));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}