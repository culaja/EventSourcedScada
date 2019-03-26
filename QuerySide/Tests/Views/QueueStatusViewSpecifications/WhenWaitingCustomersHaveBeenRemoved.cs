using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications
{
    public sealed class WhenWaitingCustomersHaveBeenRemoved : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket1Id);
            yield return new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket2Id);
            yield return new TicketIssued(TicketIssuerId, Ticket3Id, Ticket3Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket3Id);
            yield return new WaitingCustomersRemoved(CustomerQueueId, new [] {Ticket1Id, Ticket2Id, Ticket3Id});
        }

        [Fact]
        public void queue_is_empty() => View.WaitingCustomers.Should().BeEmpty();
    }
}