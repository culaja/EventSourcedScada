using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.RevokeCustomerSpecifications
{
    public sealed class WhenCustomerIsAlreadyRevoked : CustomerQueueSpecification<RevokeCustomer>
    {
        public WhenCustomerIsAlreadyRevoked() : base(SingleCustomerQueueId)
        {
        }

        protected override RevokeCustomer CommandToExecute => new RevokeCustomer(CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_TakenTimestamp);
            yield return new CustomerRevoked(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        }

        public override CommandHandler<RevokeCustomer> When() => new RevokeCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer_revoked_event_is_not_produced() => ProducedEvents.Should().NotContain(new CustomerRevoked(
            SingleCustomerQueueId,
            CounterA_Name,
            Ticket1_Id));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}