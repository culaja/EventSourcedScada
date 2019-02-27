using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
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
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Name);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
            yield return new CustomerServed(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);
        
        [Fact]
        public void customer_served_event_not_produced_again_for_the_same_ticket() => ProducedEvents.Should().NotContain(new CustomerServed(
            SingleCustomerQueueId,
            CounterA_Name,
            Ticket1_Id));
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}