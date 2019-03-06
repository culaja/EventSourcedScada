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
        public void Counter1_is_added() => ProducedEvents.Should().Contain(Counter1Added);
    }
}