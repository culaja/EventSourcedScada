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
    public sealed class WhenFullConfigurationIsSetAndSettingPartialConfiguration : CustomerQueueSpecification<SetCounterConfiguration>
    {
        public WhenFullConfigurationIsSetAndSettingPartialConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetCounterConfiguration CommandToExecute => new SetCounterConfiguration(TwoCounterConfiguration);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
        }

        public override CommandHandler<SetCounterConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Counter3_is_removed() => ProducedEvents.Should().Contain(Counter3Removed);

        [Fact]
        public void Counter1_is_not_removed() => ProducedEvents.Should().NotContain(Counter1Removed);

        [Fact]
        public void Counter2_is_not_removed() => ProducedEvents.Should().NotContain(Counter2Removed);

        [Fact]
        public void no_counters_are_added() => ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<CounterAdded>());
    }
}