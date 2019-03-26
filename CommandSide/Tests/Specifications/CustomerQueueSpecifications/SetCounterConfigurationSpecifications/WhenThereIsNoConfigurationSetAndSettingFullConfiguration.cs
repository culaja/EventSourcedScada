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
    public sealed class WhenThereIsNoConfigurationSetAndSettingFullConfiguration : CustomerQueueSpecification<SetCounterConfiguration>
    {
        public WhenThereIsNoConfigurationSetAndSettingFullConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetCounterConfiguration CommandToExecute => new SetCounterConfiguration(ThreeCounterConfiguration);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<SetCounterConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllCounters_are_added() => ProducedEvents.Should().ContainInOrder(AllCountersAdded);
    }
}