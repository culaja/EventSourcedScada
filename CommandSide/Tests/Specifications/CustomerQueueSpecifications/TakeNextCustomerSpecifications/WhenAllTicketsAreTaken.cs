using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.TakeNextCustomerSpecifications
{
    public sealed class WhenAllTicketsAreTaken : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenAllTicketsAreTaken() : base(SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CounterA_Name, CounterA_TakeNextCustomerTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_TakenTimestamp);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer_taken_is_not_produced() => ProducedEvents.Should().NotContain(new CustomerTaken(
            SingleCustomerQueueId,
            CounterA_Name,
            Ticket1_Id,
            CounterA_TakeNextCustomerTimestamp));

        [Fact]
        public void returns_failure() => Result.IsSuccess.Should().BeTrue();
    }
}