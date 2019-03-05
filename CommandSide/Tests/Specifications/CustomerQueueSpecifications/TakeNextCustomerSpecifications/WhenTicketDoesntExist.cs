using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.TakeNextCustomerSpecifications
{
    public sealed class WhenTicketDoesntExistInQueue : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenTicketDoesntExistInQueue() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CustomerQueueTestValues.CounterA_Name);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_produce_customer_served() => ProducedEvents.Should().NotBeOfType<CustomerTaken>();
    }
}