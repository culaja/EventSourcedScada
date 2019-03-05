using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.TakeNextCustomerSpecifications
{
    public sealed class WhenCounterAlreadyTakenSomeTicket : CustomerQueueSpecification<TakeNextCustomer>
    {
        public WhenCounterAlreadyTakenSomeTicket() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override TakeNextCustomer CommandToExecute => new TakeNextCustomer(CustomerQueueTestValues.CounterA_Name);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
            yield return new TicketAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number);
            yield return new TicketAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.Ticket2_Id, CustomerQueueTestValues.Ticket2_Number);
            yield return new CustomerTaken(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name, CustomerQueueTestValues.Ticket1_Id);
        }

        public override CommandHandler<TakeNextCustomer> When() => new TakeNextCustomerHandler(CustomerQueueRepository);
        
        [Fact]
        public void customer_served_event_produced_for_previous_ticket() => ProducedEvents.Should().Contain(new CustomerServed(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.CounterA_Name,
            CustomerQueueTestValues.Ticket1_Id));
        
        [Fact]
        public void customer_taken_is_produced_for_next_ticket() => ProducedEvents.Should().Contain(new CustomerTaken(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.CounterA_Name,
            CustomerQueueTestValues.Ticket2_Id));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}