using System;
using System.Collections.Generic;
using CommandSide.Domain.Queueing;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<AddCounter>
    {
        public WhenCounterDoesntExist() : base(SingleCustomerQueueId)
        {
        }

        protected override AddCounter CommandToExecute => new AddCounter(Counter1Id);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void return_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produces_counter_1_added_event() => ProducedEvents.Should().Contain(Counter1Added);
    }
}