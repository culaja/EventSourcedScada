using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Shared.CustomerQueue.Events;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class WhenCounterAlreadyExists : CustomerQueueSpecification<AddCounter>
    {
        public WhenCounterAlreadyExists() : base(SingleCustomerQueueId)
        {
        }

        protected override AddCounter CommandToExecute => new AddCounter(Counter1Id);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void doesnt_produce_counter_added_event() => ProducedEvents.Should().NotContain(EventOf<CounterAdded>());
    }
}