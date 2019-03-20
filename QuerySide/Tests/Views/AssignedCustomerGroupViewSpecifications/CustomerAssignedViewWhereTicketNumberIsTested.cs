using System.Collections.Generic;
using Common.Messaging;
using FluentAssertions;
using QuerySide.Views.AssigningCustomer;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;
using Xunit;

namespace QuerySide.Tests.Views.AssignedCustomerGroupViewSpecifications
{
    public sealed class WhenTicketNumberIsTested : GroupViewSpecification<AssignedCustomerGroupView>
    {
        protected override IEnumerable<IDomainEvent> WhenApplied()
        {
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Ticket1Number);
            yield return new TicketIssued(CustomerQueueViewsTestValues.TicketIssuerId, CustomerQueueViewsTestValues.Ticket2Id, CustomerQueueViewsTestValues.Ticket2Number);
            yield return new CustomerEnqueued(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id);
            yield return new CustomerEnqueued(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket2Id);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket1Id, CustomerQueueViewsTestValues.Counter1Id);
            yield return new CustomerAssignedToCounter(CustomerQueueViewsTestValues.CustomerQueueId, CustomerQueueViewsTestValues.Ticket2Id, CustomerQueueViewsTestValues.Counter1Id);
        }
        
        [Fact]
        public void ticket_number_should_be_2() => 
            GroupView.GenerateViewFor(CustomerQueueViewsTestValues.Counter1Id.ToCounterId()).TicketNumber.Should().Be(CustomerQueueViewsTestValues.Ticket2Number);
    }
}