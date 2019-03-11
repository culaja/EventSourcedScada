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
    public sealed class WhenCountersNamesAlreadyChanged : CustomerQueueSpecification<SetConfiguration>
    {
        public WhenCountersNamesAlreadyChanged() : base(SingleCustomerQueueId)
        {
        }
    
        protected override SetConfiguration CommandToExecute => new SetConfiguration(FullConfigurationWithAllChangedCounterNames);
    
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            foreach (var item in AllCountersAdded) yield return item;
            foreach (var item in AllOpenTimesAdded) yield return item;
            yield return Counter1NameChanged;
            yield return Counter2NameChanged;
            yield return Counter3NameChanged;
        }
    
        public override CommandHandler<SetConfiguration> When() => new SetConfigurationHandler(CustomerQueueRepository);
        
        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    
        [Fact]
        public void Counter1_name_has_not_changed() => ProducedEvents.Should().NotContain(Counter1NameChanged);
        
        [Fact]
        public void Counter2_name_has_not_changed() => ProducedEvents.Should().NotContain(Counter2NameChanged);
        
        [Fact]
        public void Counter3_name_has_not_changed() => ProducedEvents.Should().NotContain(Counter3NameChanged);
    }
}