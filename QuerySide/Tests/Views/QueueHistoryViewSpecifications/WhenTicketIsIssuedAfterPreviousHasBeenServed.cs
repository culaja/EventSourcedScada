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
    public sealed class WhenTicketIsIssuedAfterPreviousHasBeenServed : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp);
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket2Id, CustomerQueueViewsTestValues.Ticket2Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket2IssuedTimestamp);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.Ticket1AssignedTimestamp);
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket3Id, CustomerQueueViewsTestValues.Ticket3Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket3IssuedTimestamp);
        }

        [Fact]
        public void waiting_customer_count_for_last_ticket_is_1() => View.TicketHistory.Last()
            .WaitingCustomerCount.Should().Be(1);
    }
}