using System;
using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Domain.Queueing.Configuring.Configuration;
using static CommandSide.Tests.Specifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetConfigurationSpecifications
{
    public sealed class WhenFullConfigurationIsSetAndSettingPartialConfiguration : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenFullConfigurationIsSetAndSettingPartialConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute =>  new SetConfiguration(ConfigurationWithMondayOpenTimesAndFirstTwoCounters);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
            foreach (var item in AllOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Counter3_is_removed() => ProducedEvents.Should().Contain(Counter3Removed);

        [Fact]
        public void Counter1_is_not_removed() => ProducedEvents.Should().NotContain(Counter1Removed);
        
        [Fact]
        public void Counter2_is_not_removed() => ProducedEvents.Should().NotContain(Counter2Removed);

        [Fact]
        public void Tuesday9To12_is_removed() => ProducedEvents.Should().Contain(Tuesday9To12Removed);

        [Fact]
        public void Monday9To12_is_not_removed() => ProducedEvents.Should().NotContain(Monday9To12Removed);
        
        [Fact]
        public void Monday14To16_is_not_removed() => ProducedEvents.Should().NotContain(Monday14To16Removed);
    }
}