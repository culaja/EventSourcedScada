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
    public sealed class WhenAllTicketsAreTaken : CustomerQueueSpecification<TakeNextCustomer>
    {
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Id, CounterA_TakeNextCustomerTimestamp);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CounterA_Id, CounterA_Name);
            yield return new TicketAdded(AggregateRootId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new CustomerTaken(AggregateRootId, CounterA_Id, Ticket1_Id);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer_taken_is_not_produced() => ProducedEvents.Should().NotContain(new CustomerTaken(
            AggregateRootId,
            CounterA_Id,
            Ticket1_Id));

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();
    }
}