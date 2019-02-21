using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.RevokeCustomerSpecifications
{
    public sealed class WhenCounterDoesntExist : CustomerQueueSpecification<RevokeCustomer>
    {
        public WhenCounterDoesntExist() : base(SingleCustomerQueueId)
        {
        }

        protected override RevokeCustomer CommandToExecute => new RevokeCustomer(CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<RevokeCustomer> When() => new RevokeCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}