using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.RevokeCustomerSpecifications
{
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<RevokeCustomer>
    {
        public WhenCounterDoesntExist() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }

        protected override RevokeCustomer CommandToExecute => new RevokeCustomer(CustomerQueueTestValues.CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<RevokeCustomer> When() => new RevokeCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}