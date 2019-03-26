using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueHistory;
using Shared.TicketIssuer.Events;
using Xunit;

namespace QuerySide.Tests.Views.QueueHistoryViewSpecifications
{
    public sealed class WhenOutOfLineTicketIsIssuedAfterTicketIsIssued : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp);
            yield return new OutOfLineTicketIssued(
                CustomerQueueViewsTestValues.TicketIssuerId,
                CustomerQueueViewsTestValues.OutOfLineTicket10kId,
                CustomerQueueViewsTestValues.OutOfLineTicket10kNumber,
                CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.OutOfLineTicket10kIdIssuedTimestamp);
        }

        [Fact]
        public void waiting_customer_count_for_last_ticket_is_1() => View.TicketHistory.Last()
            .WaitingCustomerCount.Should().Be(1);
    }
}