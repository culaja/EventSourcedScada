using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.ServeOutOfLineCustomerSpecifications
{
    public sealed class WhenOpenedCounterExistsWithoutServingAnyCustomer : CustomerQueueSpecification<ServeOutOfLineCustomer>
    {
        public WhenOpenedCounterExistsWithoutServingAnyCustomer() : base(SingleCustomerQueueId)
        {
        }

        protected override ServeOutOfLineCustomer CommandToExecute => new ServeOutOfLineCustomer(Counter1Id, Customer1TicketId);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
        }

        public override CommandHandler<ServeOutOfLineCustomer> When() => new ServeOutOfLineCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void out_of_line_customer1_has_been_assigned_to_counter_1() => ProducedEvents.Should().Contain(OutOfLineCustomer1AssignedToCounter(Counter1Id));
    }
}