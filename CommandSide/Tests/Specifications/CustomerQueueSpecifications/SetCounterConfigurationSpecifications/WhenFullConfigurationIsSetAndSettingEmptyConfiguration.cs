using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Domain.CounterConfiguration;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetCounterConfigurationSpecifications
{
    public sealed class WhenFullConfigurationIsSetAndSettingEmptyConfiguration : CustomerQueueSpecification<SetCounterConfiguration>
    {
        public WhenFullConfigurationIsSetAndSettingEmptyConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetCounterConfiguration CommandToExecute => new SetCounterConfiguration(EmptyCounterConfiguration);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
        }

        public override CommandHandler<SetCounterConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllCounters_are_removed() => ProducedEvents.Should().ContainInOrder(AllCountersRemoved);
    }
}