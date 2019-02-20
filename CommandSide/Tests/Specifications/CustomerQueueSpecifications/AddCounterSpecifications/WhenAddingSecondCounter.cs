using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingSecondCounterSpecification : CustomerQueueSpecification<AddCounter>
    {
        public AddingSecondCounterSpecification() : base(SingleCustomerQueueId)
        {
        }
        
        protected override AddCounter CommandToExecute => new AddCounter(CounterB_Id, CounterB_Name);
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Id, CounterA_Name);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);
        
        [Fact]
        public void contains_counter_added_event() => ProducedEvents.Should().Contain(new CounterAdded(
            SingleCustomerQueueId,
            CounterB_Id,
            CounterB_Name));

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();
    }
}