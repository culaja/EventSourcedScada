using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.CloseCounterSpecifications
{
    public sealed class WhenCounterExistsAndItIsOpened : CustomerQueueSpecification<CloseCounter>
    {
        public WhenCounterExistsAndItIsOpened() : base(SingleCustomerQueueId)
        {
        }

        protected override CloseCounter CommandToExecute => new CloseCounter(Counter1Id);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
        }

        public override CommandHandler<CloseCounter> When() => new CloseCounterHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Counter1_is_closed() => ProducedEvents.Should().Contain(Counter1Closed);
    }
}