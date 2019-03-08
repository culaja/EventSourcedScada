using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetConfigurationSpecifications
{
    public sealed class WhenThereIsNoConfigurationSetAndSettingFullConfiguration : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenThereIsNoConfigurationSetAndSettingFullConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute => new SetConfiguration(FullConfiguration);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllCounters_are_added() => ProducedEvents.Should().ContainInOrder(AllCountersAdded);

        [Fact]
        public void AllOpenTimes_are_added() => ProducedEvents.Should().ContainInOrder(AllOpenTimesAdded);
    }
}