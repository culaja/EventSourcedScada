using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications.WhenTwoOpenedAndOneClosedCounter
{
    public sealed class AssertCounterAlias : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueId, Counter1Id, Counter1Name);
            yield return new CounterAdded(CustomerQueueId, Counter2Id, Counter2Name);
            yield return new CounterAdded(CustomerQueueId, Counter3Id, Counter3Name);
            yield return new CounterOpened(CustomerQueueId, Counter1Id);
            yield return new CounterOpened(CustomerQueueId, Counter2Id);
        }

        [Fact]
        public void Counter_aliases_are_C1_C2_and_C3() => View.CounterStatuses.Select(cs => cs.AliasName)
            .Should().BeEquivalentTo(Counter1Name, Counter2Name, Counter3Name);
    }
}