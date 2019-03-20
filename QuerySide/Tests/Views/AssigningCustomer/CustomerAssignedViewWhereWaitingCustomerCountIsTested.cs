using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.AssigningCustomer;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.AssigningCustomer
{
    public sealed class WhenWaitingCustomerCountIsTested : GroupViewSpecification<AssignedCustomerGroupView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number);
            yield return new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number);
            yield return new TicketIssued(TicketIssuerId, Ticket3Id, Ticket3Number);
            yield return new TicketIssued(TicketIssuerId, Ticket4Id, Ticket4Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket1Id);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket2Id);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket3Id);
            yield return new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket4Id);
        }
        
        [Fact]
        public void waiting_customer_count_is_3() => 
            GroupView.GenerateViewFor(Counter1Id.ToCounterId()).WaitingCustomerCount.Should().Be(3);
    }
}