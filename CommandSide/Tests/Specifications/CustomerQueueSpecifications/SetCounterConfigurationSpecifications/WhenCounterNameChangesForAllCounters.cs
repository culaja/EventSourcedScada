using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Shared.CustomerQueue.Events;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetCounterConfigurationSpecifications
{
    public sealed class WhenCounterNameChangesForAllCounters : CustomerQueueSpecification<SetCounterConfiguration>
    {
        public WhenCounterNameChangesForAllCounters() : base(SingleCustomerQueueId)
        {
        }

        protected override SetCounterConfiguration CommandToExecute => new SetCounterConfiguration(ThreeCounterConfigurationWithAllChangedNames);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
        }

        public override CommandHandler<SetCounterConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllCounters_changed_name() => ProducedEvents.Should().ContainInOrder(AllCountersNamesChanged);

        [Fact]
        public void no_counter_is_added() => ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<CounterAdded>());

        [Fact]
        public void no_counter_is_removed() => ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<CounterRemoved>());
    }
}