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
    public sealed class AddingCounterWhenCounterAlreadyExistsSpecification : CustomerQueueSpecification<AddCounter>
    {
        protected override AddCounter CommandToExecute { get; } = new AddCounter(Guid.NewGuid(), "Counter1");
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CommandToExecute.CounterId, CommandToExecute.CounterName);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_contain_counter_added_event() => ProducedEvents.Should().NotContain(new CounterAdded(
            AggregateRootId,
            CommandToExecute.CounterId,
            CommandToExecute.CounterName));

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}