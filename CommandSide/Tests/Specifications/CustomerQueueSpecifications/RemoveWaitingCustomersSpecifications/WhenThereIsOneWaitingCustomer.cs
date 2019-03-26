using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RemoveWaitingCustomersSpecifications
{
    public sealed class WhenThereIsOneWaitingCustomer : CustomerQueueSpecification<RemoveWaitingCustomers>
    {
        public WhenThereIsOneWaitingCustomer() : base(SingleCustomerQueueId)
        {
        }

        protected override RemoveWaitingCustomers CommandToExecute => new RemoveWaitingCustomers();

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Customer1Enqueued;
        }

        public override CommandHandler<RemoveWaitingCustomers> When() => new RemoveWaitingCustomersHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void customer1_removed() => ProducedEvents.Should().Contain(WaitingCustomersRemoved(Customer1TicketId));
    }
}