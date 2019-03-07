using FluentAssertions;
using QuerySide.Views.CustomerQueueViews.Configuring;
using Shared.CustomerQueue;
using Xunit;
using static System.DayOfWeek;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.Configuring
{
    public sealed class ConfigurationViewWhereOpenTimesAreTested
    {
        private readonly ConfigurationView _configurationView = new ConfigurationView()
            .Apply(new OpenTimeAdded(CustomerQueueId, Monday, Hour9, Hour12))
            .Apply(new OpenTimeAdded(CustomerQueueId, Monday, Hour14, Hour16))
            .Apply(new OpenTimeAdded(CustomerQueueId, Tuesday, Hour9, Hour12))
            .Apply(new OpenTimeRemoved(CustomerQueueId, Monday, Hour14, Hour16))
            as ConfigurationView;

        [Fact]
        public void should_contain_2_open_time_items() => _configurationView.OpenTimes.Should().HaveCount(2);

        [Fact]
        public void first_open_time_is_in_configuration() => _configurationView.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(Monday, Hour9, Hour12));
        
        [Fact]
        public void second_open_time_is_not_in_configuration() => _configurationView.OpenTimes.Should().NotContain(
            new OpenTimeConfiguration(Monday, Hour14, Hour16));
        
        [Fact]
        public void third_open_time_is_in_configuration() => _configurationView.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(Tuesday, Hour9, Hour12));
    }
}