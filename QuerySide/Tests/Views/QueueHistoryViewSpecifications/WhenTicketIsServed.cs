using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueHistory;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;

namespace QuerySide.Tests.Views.QueueHistoryViewSpecifications
{
    public sealed class WhenTicketIsServed : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.Ticket1AssignedTimestamp);
            yield return new CustomerServedByCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.Ticket1ServedTimestamp);
        }

        [Fact]
        public void FinishTime_is_set() => View.TicketHistory.First().FinishTime.Should().Be(CustomerQueueViewsTestValues.Ticket1ServedTimestamp);
    }
}