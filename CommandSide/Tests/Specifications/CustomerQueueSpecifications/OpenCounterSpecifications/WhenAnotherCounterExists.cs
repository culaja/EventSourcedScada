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

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.OpenCounterSpecifications
{
    public sealed class WhenAnotherCounterExists : CustomerQueueSpecification<OpenCounter>
    {
        public WhenAnotherCounterExists() : base(SingleCustomerQueueId)
        {
        }

        protected override OpenCounter CommandToExecute => new OpenCounter(Counter2Id); 
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
        }

        public override CommandHandler<OpenCounter> When() => new OpenCounterHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void no_counter_is_opened() => ProducedEvents.Should().NotContain(EventOf<CounterOpened>());
    }
}