using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingCounterSpecification : CustomerQueueSpecification<AddCounter>
    {
        public AddingCounterSpecification() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override AddCounter CommandToExecute => new AddCounter(CustomerQueueTestValues.CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void contains_counter_added_event() => ProducedEvents.Should().Contain(new CounterAdded(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.CounterA_Name));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}