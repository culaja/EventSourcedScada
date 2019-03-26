using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.EnqueueCustomerSpecifications
{
    public sealed class InAnyCase : CustomerQueueSpecification<EnqueueCustomer>
    {
        public InAnyCase() : base(SingleCustomerQueueId)
        {
        }

        protected override EnqueueCustomer CommandToExecute => new EnqueueCustomer(Customer1TicketId);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<EnqueueCustomer> When() => new EnqueueCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void enqueues_new_customer() => ProducedEvents.Should().Contain(Customer1Enqueued);
    }
}