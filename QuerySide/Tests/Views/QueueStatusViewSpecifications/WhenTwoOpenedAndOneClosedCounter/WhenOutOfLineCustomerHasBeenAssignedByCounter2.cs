using System.Collections.Generic;
using System.Linq;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.QueueStatus;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;
using static QuerySide.Tests.Views.CustomerQueueViewsTestValues;

namespace QuerySide.Tests.Views.QueueStatusViewSpecifications.WhenTwoOpenedAndOneClosedCounter
{
    public sealed class WhenOutOfLineCustomerHasBeenAssignedByCounter2 : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueId, Counter1Id, Counter1Name);
            yield return new CounterAdded(CustomerQueueId, Counter2Id, Counter2Name);
            yield return new CounterAdded(CustomerQueueId, Counter3Id, Counter3Name);
            yield return new CounterOpened(CustomerQueueId, Counter1Id);
            yield return new CounterOpened(CustomerQueueId, Counter2Id);

            yield return new OutOfLineTicketIssued(TicketIssuerId,
                OutOfLineTicket10kId,
                OutOfLineTicket10kNumber,
                Counter2Id);
            yield return new OutOfLineCustomerAssignedToCounter(CustomerQueueId, OutOfLineTicket10kId, Counter2Id);
        }

        [Fact]
        public void Counter1_is_serving__no_tickets() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter1Id)
            .LastTicketNumber.Should().Be(0);

        [Fact]
        public void Counter2_is_serving_Ticket10k() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter2Id)
            .LastTicketNumber.Should().Be(OutOfLineTicket10kNumber);

        [Fact]
        public void Counter3_is_serving_no_tickets() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter3Id)
            .LastTicketNumber.Should().Be(0);
    }
}