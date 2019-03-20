using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.Configuring;
using Shared.CustomerQueue.Events;
using Xunit;

namespace QuerySide.Tests.Views.ConfigurationViewSpecifications
{
    public sealed class WhenCountersAreTested : ViewSpecification<ConfigurationView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, 1, "Counter1");
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, 2, "Counter2");
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, 3, "Counter3");
            yield return new CounterRemoved(CustomerQueueViewsTestValues.CustomerQueueId, 2);
            yield return new CounterNameChanged(CustomerQueueViewsTestValues.CustomerQueueId, 3, "Counter3NewName");
        }
        
        [Fact]
        public void should_contain_2_counter_items() => View.Counters.Should().HaveCount(2);

        [Fact]
        public void counter1_is_in_configuration() => View.Counters.Should().Contain(
            new CounterConfiguration(1, "Counter1"));
        
        [Fact]
        public void counter2_is_not_in_configuration() => View.Counters.Should().NotContain(
            new CounterConfiguration(2, "Counter3"));
        
        [Fact]
        public void counter3_is_in_configuration_with_changed_name() => View.Counters.Should().Contain(
            new CounterConfiguration(3, "Counter3NewName"));
    }
}