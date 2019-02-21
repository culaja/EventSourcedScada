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
    public sealed class WhenCustomerIsServedAndThereIsNoOtherTicketsPending : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenCustomerIsServedAndThereIsNoOtherTicketsPending() : base(SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Name, Ticket1_ServedTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_TakenTimestamp);
            yield return new CustomerServed(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_ServedTimestamp);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);
        
        [Fact]
        public void customer_served_event_not_produced_again_for_the_same_ticket() => ProducedEvents.Should().NotContain(new CustomerServed(
            SingleCustomerQueueId,
            CounterA_Name,
            Ticket1_Id,
            Ticket1_ServedTimestamp));
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}