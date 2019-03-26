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
    public sealed class WhenOutOfLineCustomerIsAssignedToCounter : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new OutOfLineTicketIssued(
                CustomerQueueViewsTestValues.TicketIssuerId,
                CustomerQueueViewsTestValues.OutOfLineTicket10kId,
                CustomerQueueViewsTestValues.OutOfLineTicket10kNumber,
                CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.OutOfLineTicket10kIdIssuedTimestamp);
            yield return new OutOfLineCustomerAssignedToCounter(
                CustomerQueueViewsTestValues.CustomerQueueId,
                CustomerQueueViewsTestValues.OutOfLineTicket10kId,
                CustomerQueueViewsTestValues.Counter1Id).SetTimestamp(CustomerQueueViewsTestValues.OutOfLineTicket10kIdAssignedTimestamp);
        }

        [Fact]
        public void out_of_line_ticket_number_is_set() => View.TicketHistory.First()
            .TicketNumber.Should().Be(CustomerQueueViewsTestValues.OutOfLineTicket10kNumber);

        [Fact]
        public void out_of_line_customer_number_set() => View.TicketHistory.First()
            .CounterNumber.Should().Be(CustomerQueueViewsTestValues.Counter1Id);
    }
}