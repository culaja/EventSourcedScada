using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingCounterWhenCounterAlreadyExistsSpecification : CustomerQueueSpecification<AddCounter>
    {
        protected override AddCounter CommandToExecute => new AddCounter(CounterA_Id, CounterA_Name);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(AggregateRootId, CounterA_Id, CounterA_Name);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_contain_counter_added_event() => ProducedEvents.Should().NotContain(new CounterAdded(
            AggregateRootId,
            CounterA_Id,
            CounterA_Name));

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}