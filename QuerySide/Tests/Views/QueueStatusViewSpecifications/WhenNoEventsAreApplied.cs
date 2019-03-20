using System;
using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Xunit;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications
{
    public sealed class WhenNoEventsAreApplied : ViewSpecification<QueueStatusView>
    {
        private readonly DateTime _lastCashedTime = DateTime.Now; 
        
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield break;
        }

        [Fact]
        public void WaitingCustomers_is_empty() => View.WaitingCustomers.Should().BeEmpty();

        [Fact]
        public void CounterStatuses_is_empty() => View.CounterStatuses.Should().BeEmpty();

        [Fact]
        public void ExpectedWaitingTimeInSeconds_is_zero() => View.ExpectedWaitingTimeInSeconds.Should().Be(0);

        [Fact]
        public void CurrentTime_should_be_after_than_last_cashed_time() => View.CurrentTime.Should().BeAfter(_lastCashedTime);
    }
}