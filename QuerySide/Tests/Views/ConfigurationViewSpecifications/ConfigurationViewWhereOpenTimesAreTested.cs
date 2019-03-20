using System;
using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.Configuring;
using Shared.TicketIssuer.Events;
using Xunit;

namespace QuerySide.Tests.Views.ConfigurationViewSpecifications
{
    public sealed class WhenOpenTimesAreTested : ViewSpecification<ConfigurationView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new OpenTimeAdded(CustomerQueueViewsTestValues.CustomerQueueId, DayOfWeek.Monday, CustomerQueueViewsTestValues.Hour9, CustomerQueueViewsTestValues.Hour12);
            yield return new OpenTimeAdded(CustomerQueueViewsTestValues.CustomerQueueId, DayOfWeek.Monday, CustomerQueueViewsTestValues.Hour14, CustomerQueueViewsTestValues.Hour16);
            yield return new OpenTimeAdded(CustomerQueueViewsTestValues.CustomerQueueId, DayOfWeek.Tuesday, CustomerQueueViewsTestValues.Hour9, CustomerQueueViewsTestValues.Hour12);
            yield return new OpenTimeRemoved(CustomerQueueViewsTestValues.CustomerQueueId, DayOfWeek.Monday, CustomerQueueViewsTestValues.Hour14, CustomerQueueViewsTestValues.Hour16);
        }
        
        [Fact]
        public void should_contain_2_open_time_items() => View.OpenTimes.Should().HaveCount(2);

        [Fact]
        public void first_open_time_is_in_configuration() => View.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(DayOfWeek.Monday, CustomerQueueViewsTestValues.Hour9, CustomerQueueViewsTestValues.Hour12));
        
        [Fact]
        public void second_open_time_is_not_in_configuration() => View.OpenTimes.Should().NotContain(
            new OpenTimeConfiguration(DayOfWeek.Monday, CustomerQueueViewsTestValues.Hour14, CustomerQueueViewsTestValues.Hour16));
        
        [Fact]
        public void third_open_time_is_in_configuration() => View.OpenTimes.Should().Contain(
            new OpenTimeConfiguration(DayOfWeek.Tuesday, CustomerQueueViewsTestValues.Hour9, CustomerQueueViewsTestValues.Hour12));
    }
}