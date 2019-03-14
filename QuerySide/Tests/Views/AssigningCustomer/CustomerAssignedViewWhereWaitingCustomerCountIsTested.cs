using FluentAssertions;
using QuerySide.Views.AssigningCustomer;
using Shared.CustomerQueue;
using Shared.TicketIssuer;
using Xunit;
using static Tests.Views.CustomerQueueViewsTestValues;

namespace Tests.Views.AssigningCustomer
{
    public sealed class CustomerAssignedViewWhereWaitingCustomerCountIsTested
    {
        private readonly AssignedCustomerView _assignedCustomerView = new AssignedCustomerView()
                .Apply(new TicketIssued(TicketIssuerId, Ticket1Id, Ticket1Number))
                .Apply(new TicketIssued(TicketIssuerId, Ticket2Id, Ticket2Number))
                .Apply(new TicketIssued(TicketIssuerId, Ticket3Id, Ticket3Number))
                .Apply(new TicketIssued(TicketIssuerId, Ticket4Id, Ticket4Number))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket1Id))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket2Id))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket3Id))
                .Apply(new CustomerAssignedToCounter(CustomerQueueId, Ticket1Id, Counter1Id))
                .Apply(new CustomerEnqueued(CustomerQueueId, Ticket4Id))
            as AssignedCustomerView;

        [Fact]
        public void waiting_customer_count_is_3() => _assignedCustomerView.WaitingCustomerCount.Should().Be(3);
    }
}