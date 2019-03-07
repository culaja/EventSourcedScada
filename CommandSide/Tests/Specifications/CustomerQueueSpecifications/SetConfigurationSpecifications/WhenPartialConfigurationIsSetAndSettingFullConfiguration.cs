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
    public sealed class WhenPartialConfigurationIsSetAndSettingFullConfiguration : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenPartialConfigurationIsSetAndSettingFullConfiguration() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute =>  new SetConfiguration(FullConfiguration);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in FirstTwoCountersAdded) yield return item;
            foreach (var item in MondayOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void Customer3_is_Added() => ProducedEvents.Should().Contain(Counter3Added);

        [Fact]
        public void Counter1_is_not_Added() => ProducedEvents.Should().NotContain(Counter1Added);
        
        [Fact]
        public void Counter2_is_not_Added() => ProducedEvents.Should().NotContain(Counter2Added);
    }
}