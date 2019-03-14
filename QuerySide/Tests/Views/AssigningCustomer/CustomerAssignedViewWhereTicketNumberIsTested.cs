using FluentAssertions;
using QuerySide.Views.AssigningCustomer;
using Shared.CustomerQueue;
using Shared.TicketIssuer;
using Xunit;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.AssigningCustomer
{
    public sealed class CustomerAssignedViewWhereTicketNumberIsTested
    {
        private readonly AssignedCustomerView _assignedCustomerView = new AssignedCustomerView()
                .Apply(new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number))
                .Apply(new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket1Id))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket2Id))
                .Apply(new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id))
                .Apply(new CustomerServedByCounter(CustomerQueueId, Ticket1Id, Counter1Id))
                .Apply(new CustomerAssignedToCounter(CustomerQueueId, Ticket2Id, Counter1Id))
            as AssignedCustomerView;

        [Fact]
        public void ticket_number_should_be_2() => _assignedCustomerView.TicketNumber.Should().Be(Ticket2Number);
    }
}