using System;
using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace Tests.Specifications.CustomerQueueSpecifications
{
    public sealed class AddingCounterSpecification : CustomerQueueSpecification<AddCounter>
    {
        protected override AddCounter CommandToExecute => new AddCounter(Guid.NewGuid(), "Counter1");

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void contains_counter_added_event() => ProducedEvents.Should().Contain(new CounterAdded(
            AggregateRootId,
            CommandToExecute.CounterId,
            CommandToExecute.CounterName));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}