using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Shared.CustomerQueue.Events;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RecallCustomerSpecifications
{
    public sealed class WhenOpenedCounterDoesntHaveAssignedCustomers : CustomerQueueSpecification<ReCallCustomer>
    {
        public WhenOpenedCounterDoesntHaveAssignedCustomers() : base(SingleCustomerQueueId)
        {
        }

        protected override ReCallCustomer CommandToExecute => new ReCallCustomer(Counter1Id);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
        }

        public override CommandHandler<ReCallCustomer> When() => new RecallCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void no_customer_is_recalled() => ProducedEvents.Should().NotContain(EventOf<CustomerRecalledByCounter>());
    }
}