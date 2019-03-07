using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Domain.Queueing.Configuring.Configuration;
using static CommandSide.Tests.Specifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetConfigurationSpecifications
{
    public sealed class WhenFullConfigurationIsSetAndSettingEmptyConfiguration : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenFullConfigurationIsSetAndSettingEmptyConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute => new SetConfiguration(EmptyConfiguration);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
            foreach (var item in AllOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void AllCounters_are_removed() => ProducedEvents.Should().ContainInOrder(AllCountersRemoved);
        
        [Fact]
        public void AllOpenTimes_are_removed() => ProducedEvents.Should().ContainInOrder(AllOpenTimesRemoved);
    }
}