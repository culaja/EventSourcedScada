using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingCounterWhenCounterAlreadyExistsSpecification : CustomerQueueSpecification<AddCounter>
    {
        public AddingCounterWhenCounterAlreadyExistsSpecification() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override AddCounter CommandToExecute => new AddCounter(CustomerQueueTestValues.CounterA_Name);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_contain_counter_added_event() => ProducedEvents.Should().NotContain(new CounterAdded(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.CounterA_Name));

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}