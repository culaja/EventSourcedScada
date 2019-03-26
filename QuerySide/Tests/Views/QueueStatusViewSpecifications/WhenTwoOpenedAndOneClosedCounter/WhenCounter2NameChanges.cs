using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Xunit;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications.WhenTwoOpenedAndOneClosedCounter
{
    public sealed class WhenCounter2NameChanges : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter1Id, CustomerQueueViewsTestValues.Counter1Name);
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter2Id, CustomerQueueViewsTestValues.Counter2Name);
            yield return new CounterAdded(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter3Id, CustomerQueueViewsTestValues.Counter3Name);
            yield return new CounterOpened(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter1Id);
            yield return new CounterOpened(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter2Id);
            yield return new CounterNameChanged(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Counter2Id, CustomerQueueViewsTestValues.Counter2ChangedName);
        }

        [Fact]
        public void Counter_aliases_are_C1_C2_and_C3() => View.CounterStatuses.Select(cs => cs.AliasName)
            .Should().BeEquivalentTo(CustomerQueueViewsTestValues.Counter1Name, CustomerQueueViewsTestValues.Counter2ChangedName, CustomerQueueViewsTestValues.Counter3Name);
    }
}