using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;
using static QuerySide.Views.QueueStatus.CounterStatus.StatusInternal;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications.WhenTwoOpenedAndOneClosedCounter
{
    public sealed class AssertCounterStatuses : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueId, Counter1Id, Counter1Name);
            yield return new CounterAdded(CustomerQueueId, Counter2Id, Counter2Name);
            yield return new CounterAdded(CustomerQueueId, Counter3Id, Counter3Name);
            yield return new CounterOpened(CustomerQueueId, Counter1Id);
            yield return new CounterOpened(CustomerQueueId, Counter2Id);

            yield return new CounterClosed(CustomerQueueId, Counter1Id);
            yield return new CounterClosed(CustomerQueueId, Counter2Id);
            yield return new CounterOpened(CustomerQueueId, Counter3Id);

            yield return new CounterOpened(CustomerQueueId, Counter1Id);
            yield return new CounterOpened(CustomerQueueId, Counter2Id);
            yield return new CounterClosed(CustomerQueueId, Counter3Id);
        }

        [Fact]
        public void Counter1_is_opened() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter1Id)
            .Status.Should().Be(Open);

        [Fact]
        public void Counter2_is_opened() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter2Id)
            .Status.Should().Be(Open);

        [Fact]
        public void Counter3_is_closed() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter3Id)
            .Status.Should().Be(Closed);
    }
}