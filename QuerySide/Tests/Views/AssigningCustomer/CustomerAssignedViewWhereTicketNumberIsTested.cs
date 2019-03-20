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
    public sealed class WhenTicketNumberIsTested : GroupViewSpecification<AssignedCustomerGroupView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number);
            yield return new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket1Id);
            yield return new CustomerEnqueued(CustomerQueueId, Ticket2Id);
            yield return new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id);
            yield return new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id);
            yield return new CustomerAssignedToCounter(CustomerQueueId, Ticket2Id, Counter1Id);
        }
        
        [Fact]
        public void ticket_number_should_be_2() => 
            GroupView.GenerateViewFor(Counter1Id.ToCounterId()).TicketNumber.Should().Be(Ticket2Number);
    }
}