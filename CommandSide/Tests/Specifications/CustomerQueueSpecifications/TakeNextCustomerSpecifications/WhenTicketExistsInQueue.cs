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
    public sealed class WhenTicketExistsInQueue : CustomerQueueSpecification<TakeNextCustomer>
    {
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Id, CounterA_TakeNextCustomerTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CounterA_Id, CounterA_Name);
            yield return new TicketAdded(AggregateRootId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_produce_customer_served() => ProducedEvents.Should().Contain(new CustomerTaken(
            AggregateRootId,
            CounterA_Id,
            Ticket1_Id));
    }
}