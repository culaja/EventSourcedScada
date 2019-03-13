using FluentAssertions;
using QuerySide.Views.Configuring;
using Shared.CustomerQueue;
using Xunit;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.Configuring
{
    public sealed class ConfigurationViewWhereCountersAreTested
    {
        private readonly ConfigurationView _configurationView = new ConfigurationView()
            .Apply(new CounterAdded(CustomerQueueId, 1, "Counter1"))
            .Apply(new CounterAdded(CustomerQueueId, 2, "Counter2"))
            .Apply(new CounterAdded(CustomerQueueId, 3, "Counter3"))
            .Apply(new CounterRemoved(CustomerQueueId, 2))
            .Apply(new CounterNameChanged(CustomerQueueId, 3, "Counter3NewName"))
            as ConfigurationView;
        
        [Fact]
        public void should_contain_2_counter_items() => _configurationView.Counters.Should().HaveCount(2);

        [Fact]
        public void counter1_is_in_configuration() => _configurationView.Counters.Should().Contain(
            new CounterConfiguration(1, "Counter1"));
        
        [Fact]
        public void counter2_is_not_in_configuration() => _configurationView.Counters.Should().NotContain(
            new CounterConfiguration(2, "Counter3"));
        
        [Fact]
        public void counter3_is_in_configuration_with_changed_name() => _configurationView.Counters.Should().Contain(
            new CounterConfiguration(3, "Counter3NewName"));
    }
}