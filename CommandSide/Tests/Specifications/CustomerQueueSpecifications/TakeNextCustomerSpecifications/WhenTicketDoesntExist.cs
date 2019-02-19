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
    public sealed class WhenTicketDoesntExistInQueue : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenTicketDoesntExistInQueue() : base(SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Id, CounterA_TakeNextCustomerTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Id, CounterA_Name);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_produce_customer_served() => ProducedEvents.Should().NotBeOfType<CustomerTaken>();
    }
}