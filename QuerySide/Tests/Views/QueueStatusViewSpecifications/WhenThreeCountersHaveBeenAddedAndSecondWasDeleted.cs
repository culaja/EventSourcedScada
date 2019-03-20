using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications
{
    public sealed class WhenThreeCountersHaveBeenAddedAndSecondWasDeleted : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueId, Counter1Id, Counter1Name);
            yield return new CounterAdded(CustomerQueueId, Counter2Id, Counter2Name);
            yield return new CounterAdded(CustomerQueueId, Counter3Id, Counter3Name);
            yield return new CounterRemoved(CustomerQueueId, Counter2Id);
        }

        [Fact]
        public void first_and_second_counters_are_left() => View.CounterStatuses.Select(cs => cs.CounterNumber)
            .Should().BeEquivalentTo(Counter1Id, Counter3Id);
    }
}