using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.CloseCounterSpecifications
{
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<CloseCounter>
    {
        public WhenCounterDoesntExist() : base(SingleCustomerQueueId)
        {
        }

        protected override CloseCounter CommandToExecute => new CloseCounter(Counter1Id); 
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<CloseCounter> When() => new CloseCounterHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void no_counter_is_closed() => ProducedEvents.Should().NotContain(EventOf<CloseCounter>());
    }
}