using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingSecondCounterSpecification : CustomerQueueSpecification<AddCounter>
    {
        protected override AddCounter CommandToExecute => new AddCounter(CustomerQueueTestValues.CounterB_Id, CustomerQueueTestValues.CounterB_Name);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CustomerQueueTestValues.CounterA_Id, CustomerQueueTestValues.CounterA_Name);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);
        
        [Fact]
        public void contains_counter_added_event() => ProducedEvents.Should().Contain(new CounterAdded(
            AggregateRootId,
            CustomerQueueTestValues.CounterB_Id,
            CustomerQueueTestValues.CounterB_Name));

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();
    }
}