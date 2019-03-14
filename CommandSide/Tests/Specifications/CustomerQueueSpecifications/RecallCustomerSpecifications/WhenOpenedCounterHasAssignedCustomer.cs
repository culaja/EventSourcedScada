using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RecallCustomerSpecifications
{
    public sealed class WhenOpenedCounterHasAssignedCustomer : CustomerQueueSpecification<ReCallCustomer>
    {
        public WhenOpenedCounterHasAssignedCustomer() : base(SingleCustomerQueueId)
        {
        }

        protected override ReCallCustomer CommandToExecute => new ReCallCustomer(Counter1Id);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
            yield return Customer1Enqueued;
            yield return Customer1AssignedToCounter(Counter1Id);
        }

        public override CommandHandler<ReCallCustomer> When() => new RecallCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Customer1_has_been_recalled() => ProducedEvents.Should().Contain(Customer1RecalledByCounter(Counter1Id));
    }
}