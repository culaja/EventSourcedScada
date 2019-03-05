using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RevokeCustomerSpecifications
{
    public sealed class WhenCustomerIsBeingServed : CustomerQueueSpecification<RevokeCustomer>
    {
        public WhenCustomerIsBeingServed() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }

        protected override RevokeCustomer CommandToExecute => new RevokeCustomer(CustomerQueueTestValues.CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
            yield return new TicketAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number);
            yield return new CustomerTaken(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name, CustomerQueueTestValues.Ticket1_Id);
        }

        public override CommandHandler<RevokeCustomer> When() => new RevokeCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer_revoked_event_is_produced() => ProducedEvents.Should().Contain(new CustomerRevoked(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.CounterA_Name,
            CustomerQueueTestValues.Ticket1_Id));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}