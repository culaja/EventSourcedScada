using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.TakeNextCustomerSpecifications
{
    public sealed class WhenCustomerIsServedAndThereIsNoOtherTicketsPending : CustomerQueueSpecification<TakeNextCustomer>
    {
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Id, Ticked1_ServedTimestamp);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CounterA_Id, CounterA_Name);
            yield return new TicketAdded(AggregateRootId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new CustomerTaken(AggregateRootId, CounterA_Id, Ticket1_Id);
            yield return new CustomerServed(AggregateRootId, CounterA_Id, Ticket1_Id, Ticked1_ServedTimestamp);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);
        
        [Fact]
        public void customer_served_event_not_produced_again_for_the_same_ticket() => ProducedEvents.Should().NotContain(new CustomerServed(
            AggregateRootId,
            CounterA_Id,
            Ticket1_Id,
            Ticked1_ServedTimestamp));
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}