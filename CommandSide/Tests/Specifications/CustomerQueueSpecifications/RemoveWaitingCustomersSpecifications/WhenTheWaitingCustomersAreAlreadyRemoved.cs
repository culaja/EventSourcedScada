using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RemoveWaitingCustomersSpecifications
{
    public sealed class WhenTheWaitingCustomersAreAlreadyRemoved : CustomerQueueSpecification<RemoveWaitingCustomers>
    {
        public WhenTheWaitingCustomersAreAlreadyRemoved() : base(SingleCustomerQueueId)
        {
        }

        protected override RemoveWaitingCustomers CommandToExecute => new RemoveWaitingCustomers();
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
            yield return Customer1Enqueued;
            yield return Customer2Enqueued;
            yield return Customer3Enqueued;
            yield return Customer1AssignedToCounter(Counter1Id);
            yield return WaitingCustomersRemoved(Customer2TicketId, Customer3TicketId);
        }

        public override CommandHandler<RemoveWaitingCustomers> When() => new RemoveWaitingCustomersHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void customer1_and_3_removed() => ProducedEvents.Should().NotContain(EventOf<WaitingCustomersRemoved>());
    }
}