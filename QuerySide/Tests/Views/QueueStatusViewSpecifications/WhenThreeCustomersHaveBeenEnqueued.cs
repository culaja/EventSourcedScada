using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications
{
    public sealed class WhenThreeCustomersHaveBeenEnqueued : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket1Id);
            yield return new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket2Id);
            yield return new TicketIssued(TicketIssuerId, Ticket3Id, Ticket3Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket3Id);
        }

        [Fact]
        public void WaitingCustomers_ticket_numbers_should_be_1_2_and_3() => View.WaitingCustomers.Select(wc => wc.TicketNumber)
            .Should().BeEquivalentTo(Ticket1Number, Ticket2Number, Ticket3Number);
    }
}