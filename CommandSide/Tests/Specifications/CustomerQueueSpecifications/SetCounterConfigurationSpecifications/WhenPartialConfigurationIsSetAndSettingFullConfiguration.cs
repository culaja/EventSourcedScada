using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetCounterConfigurationSpecifications
{
    public sealed class WhenPartialConfigurationIsSetAndSettingFullConfiguration : CustomerQueueSpecification<SetCounterConfiguration>
    {
        public WhenPartialConfigurationIsSetAndSettingFullConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetCounterConfiguration CommandToExecute => new SetCounterConfiguration(ThreeCounterConfiguration);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in FirstTwoCountersAdded) yield return item;
        }

        public override CommandHandler<SetCounterConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Customer3_is_added() => ProducedEvents.Should().Contain(Counter3Added);

        [Fact]
        public void Counter1_is_not_added() => ProducedEvents.Should().NotContain(Counter1Added);

        [Fact]
        public void Counter2_is_not_added() => ProducedEvents.Should().NotContain(Counter2Added);
    }
}