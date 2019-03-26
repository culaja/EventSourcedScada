using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.NextCustomerSpecifications
{
    public sealed class WhenOpenedCounterAndFirstCustomerHasBeenServedAndSecondCustomerEntered : CustomerQueueSpecification<NextCustomer>
    {
        public WhenOpenedCounterAndFirstCustomerHasBeenServedAndSecondCustomerEntered() : base(SingleCustomerQueueId)
        {
        }

        protected override NextCustomer CommandToExecute => new NextCustomer(Counter1Id);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
            yield return Customer1Enqueued;
            yield return Customer1AssignedToCounter(Counter1Id);
            yield return Customer1ServedByCounter(Counter1Id);
            yield return Customer2Enqueued;
        }

        public override CommandHandler<NextCustomer> When() => new NextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer1_is_not_served_again() => ProducedEvents.Should().NotContain(Customer1ServedByCounter(Counter1Id));

        [Fact]
        public void customer2_is_assigned() => ProducedEvents.Should().Contain(Customer2AssignedToCounter(Counter1Id));
    }
}