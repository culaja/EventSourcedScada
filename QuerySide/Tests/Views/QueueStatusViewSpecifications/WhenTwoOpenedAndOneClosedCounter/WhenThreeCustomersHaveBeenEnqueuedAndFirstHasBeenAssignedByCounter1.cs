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
    public sealed class WhenThreeCustomersHaveBeenEnqueuedAndFirstHasBeenAssignedByCounter1 : ViewSpecification<QueueStatusView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new CounterAdded(CustomerQueueId, Counter1Id, Counter1Name);
            yield return new CounterAdded(CustomerQueueId, Counter2Id, Counter2Name);
            yield return new CounterAdded(CustomerQueueId, Counter3Id, Counter3Name);
            yield return new CounterOpened(CustomerQueueId, Counter1Id);
            yield return new CounterOpened(CustomerQueueId, Counter2Id);
            
            yield return new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket1Id);
            yield return new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket2Id);
            yield return new TicketIssued(TicketIssuerId, Ticket3Id, Ticket3Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket3Id);
            yield return new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id);
        }
        
        [Fact]
        public void WaitingCustomers_ticket_numbers_should_be_2_and_3() => View.WaitingCustomers.Select(wc => wc.TicketNumber)
            .Should().BeEquivalentTo(Ticket2Number, Ticket3Number);

        [Fact]
        public void Counter1_is_serving_Ticket1() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter1Id)
            .LastTicketNumber.Should().Be(Ticket1Number);

        [Fact]
        public void Counter2_is_serving_no_tickets() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter2Id)
            .LastTicketNumber.Should().Be(0);
        
        [Fact]
        public void Counter3_is_serving_no_tickets() => View.CounterStatuses.First(cs => cs.CounterNumber == Counter3Id)
            .LastTicketNumber.Should().Be(0);
    }
}