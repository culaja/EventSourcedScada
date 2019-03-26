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
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<NextCustomer>
    {
        public WhenCounterDoesntExist() : base(SingleCustomerQueueId)
        {
        }

        protected override NextCustomer CommandToExecute => new NextCustomer(Counter1Id);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return SingleCustomerQueueCreated;
        }

        public override CommandHandler<NextCustomer> When() => new NextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}