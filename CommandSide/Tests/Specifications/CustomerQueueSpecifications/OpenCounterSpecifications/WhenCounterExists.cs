using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.OpenCounterSpecifications
{
    public sealed class WhenCounterExists : CustomerQueueSpecification<OpenCounter>
    {
        public WhenCounterExists() : base(SingleCustomerQueueId)
        {
        }

        protected override OpenCounter CommandToExecute => new OpenCounter(Counter1Id); 
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
        }

        public override CommandHandler<OpenCounter> When() => new OpenCounterHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Counter1_is_opened() => ProducedEvents.Should().Contain(Counter1Opened);
    }
}