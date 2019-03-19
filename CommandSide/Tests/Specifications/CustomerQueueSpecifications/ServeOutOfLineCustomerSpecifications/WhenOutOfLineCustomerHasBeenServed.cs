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
    public sealed class WhenOutOfLineCustomerHasBeenServed : CustomerQueueSpecification<ServeOutOfLineCustomer>
    {
        public WhenOutOfLineCustomerHasBeenServed() : base(SingleCustomerQueueId)
        {
        }

        protected override ServeOutOfLineCustomer CommandToExecute => new ServeOutOfLineCustomer(Counter1Id, Customer2TicketId);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
            yield return Counter1Added;
            yield return Counter1Opened;
            yield return OutOfLineCustomer1AssignedToCounter(Counter1Id);
        }

        public override CommandHandler<ServeOutOfLineCustomer> When() => new ServeOutOfLineCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void out_of_line_customer1_has_been_served_by_counter_1() => ProducedEvents.Should().Contain(Customer1ServedByCounter(Counter1Id));
        
        [Fact]
        public void out_of_line_customer2_has_been_served_by_counter_1() => ProducedEvents.Should().Contain(OutOfLineCustomer2AssignedToCounter(Counter1Id));
    }
}