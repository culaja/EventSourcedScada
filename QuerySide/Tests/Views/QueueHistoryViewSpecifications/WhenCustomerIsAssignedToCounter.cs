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
    public sealed class WhenCustomerIsAssignedToCounter : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.Ticket1AssignedTimestamp);
        }

        [Fact]
        public void counter_number_is_set() => View.TicketHistory.First().CounterNumber.Should().Be(CustomerQueueViewsTestValues.Counter1Id);

        [Fact]
        public void call_time_is_set() => View.TicketHistory.First().CallTime.Should().Be(CustomerQueueViewsTestValues.Ticket1AssignedTimestamp);
    }
}