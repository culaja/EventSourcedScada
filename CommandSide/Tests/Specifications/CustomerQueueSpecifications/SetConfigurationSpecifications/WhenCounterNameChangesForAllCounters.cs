using System;
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

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.SetConfigurationSpecifications
{
    public sealed class WhenCounterNameChangesForAllCounters : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenCounterNameChangesForAllCounters() : base(SingleCustomerQueueId)
        {
        }
    
        protected override SetConfiguration CommandToExecute => new SetConfiguration(FullConfigurationWithAllChangedCounterNames);
    
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
        public void AllCounters_changed_name() => ProducedEvents.Should().ContainInOrder(AllCountersNamesChanged);
    
        [Fact]
        public void no_counter_is_added() => ProducedEvents.Should().NotContain(EventOf<CounterAdded>());
    
        [Fact]
        public void no_counter_is_removed() => ProducedEvents.Should().NotContain(EventOf<CounterRemoved>());
    }
}