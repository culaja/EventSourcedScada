using System.Collections.Generic;
using CommandSide.Domain.Queueing.Commands;
using CommandSide.DomainServices.Queueing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Shared.CustomerQueue.Events;
using Xunit;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueConfigurationTestValues;
using static CommandSide.Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.ServeOutOfLineCustomerSpecifications
{
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<ServeOutOfLineCustomer>
    {
        public WhenCounterDoesntExist() : base(SingleCustomerQueueId)
        {
        }

        protected override ServeOutOfLineCustomer CommandToExecute => new ServeOutOfLineCustomer(Counter1Id, Customer1TicketId);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<ServeOutOfLineCustomer> When() => new ServeOutOfLineCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void out_of_line_customer_has_not_been_assigned() => 
            ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<OutOfLineCustomerAssignedToCounter>());
    }
}