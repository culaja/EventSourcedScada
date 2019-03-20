using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.Configuring;
using Shared.TicketIssuer.Events;
using Xunit;
using static System.DayOfWeek;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.Configuring
{
    public sealed class WhenOpenTimesAreTested : ViewSpecification<ConfigurationView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new OpenTimeAdded(CustomerQueueId, Monday, Hour9, Hour12);
            yield return new OpenTimeAdded(CustomerQueueId, Monday, Hour14, Hour16);
            yield return new OpenTimeAdded(CustomerQueueId, Tuesday, Hour9, Hour12);
            yield return new OpenTimeRemoved(CustomerQueueId, Monday, Hour14, Hour16);
        }
        
        [Fact]
        public void should_contain_2_open_time_items() => View.OpenTimes.Should().HaveCount(2);

        [Fact]
        public void first_open_time_is_in_configuration() => View.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(Monday, Hour9, Hour12));
        
        [Fact]
        public void second_open_time_is_not_in_configuration() => View.OpenTimes.Should().NotContain(
            new OpenTimeConfiguration(Monday, Hour14, Hour16));
        
        [Fact]
        public void third_open_time_is_in_configuration() => View.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(Tuesday, Hour9, Hour12));
    }
}