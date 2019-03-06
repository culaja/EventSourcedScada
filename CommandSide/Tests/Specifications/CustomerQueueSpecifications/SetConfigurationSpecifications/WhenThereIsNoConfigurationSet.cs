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
    public sealed class WhenThereIsNoConfigurationSet : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenThereIsNoConfigurationSet() : base(SingleCustomerQueueId)
        {
        }

        protected override SetConfiguration CommandToExecute => new SetConfiguration(FullConfiguration);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);

        [Fact]
        public void AllCounters_are_added() => ProducedEvents.Should().ContainInOrder(Counter1Added, Counter2Added, Counter3Added);

        [Fact]
        public void AllOpenTimes_are_added() => ProducedEvents.Should().ContainInOrder(Monday9To12Added, Monday14To16Added, Tuesday9To12Added);
    }
}