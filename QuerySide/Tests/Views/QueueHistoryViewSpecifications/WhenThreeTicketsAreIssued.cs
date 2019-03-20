using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueHistory;
using Shared.TicketIssuer.Events;
using Xunit;

namespace QuerySide.Tests.Views.QueueHistoryViewSpecifications
{
    public sealed class WhenThreeTicketsAreIssued : ViewSpecification<QueueHistoryView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp);
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket2Id, CustomerQueueViewsTestValues.Ticket2Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket2IssuedTimestamp);
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket3Id, CustomerQueueViewsTestValues.Ticket3Number).SetTimestamp(CustomerQueueViewsTestValues.Ticket3IssuedTimestamp);
        }

        [Fact]
        public void there_are_3_tickets_in_history() => View.TicketHistory.Should().HaveCount(3);

        [Fact]
        public void ticket_numbers_are_correct() => View.TicketHistory.Select(t => t.TicketNumber)
            .Should().BeEquivalentTo(CustomerQueueViewsTestValues.Ticket1Number, CustomerQueueViewsTestValues.Ticket2Number, CustomerQueueViewsTestValues.Ticket3Number);

        [Fact]
        public void tickets_draw_times_are_correct() => View.TicketHistory.Select(t => t.DrawTime)
            .Should().BeEquivalentTo(CustomerQueueViewsTestValues.Ticket1IssuedTimestamp, CustomerQueueViewsTestValues.Ticket2IssuedTimestamp, CustomerQueueViewsTestValues.Ticket3IssuedTimestamp);

        [Fact]
        public void waiting_customer_count_is_0_1_and_3() => View.TicketHistory.Select(t => t.WaitingCustomerCount)
            .Should().BeEquivalentTo(0, 1, 2);
    }
}