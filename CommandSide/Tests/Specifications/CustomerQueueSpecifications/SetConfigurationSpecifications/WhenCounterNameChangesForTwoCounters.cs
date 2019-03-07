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
    public sealed class WhenCounterNameChangesForTwoCounters : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenCounterNameChangesForTwoCounters() : base(SingleCustomerQueueId)
        {
        }
    
        protected override SetConfiguration CommandToExecute => new SetConfiguration(FullConfigurationWithTwoChangedCounterNames);
    
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
        public void Counter1_changed_name() => ProducedEvents.Should().Contain(Counter1NameChanged);
        
        [Fact]
        public void Counter2_did_not_change_name() => ProducedEvents.Should().NotContain(Counter2NameChanged);
        
        [Fact]
        public void Counter3_changed_name() => ProducedEvents.Should().Contain(Counter3NameChanged);
    }
}