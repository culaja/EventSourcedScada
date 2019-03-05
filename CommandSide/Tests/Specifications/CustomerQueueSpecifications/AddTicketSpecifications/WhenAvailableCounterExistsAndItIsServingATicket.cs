using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenAvailableCounterExistsAndItIsServingATicket : CustomerQueueSpecification<AddTicket>
    {
        public WhenAvailableCounterExistsAndItIsServingATicket() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(CustomerQueueTestValues.Ticket2_Id, CustomerQueueTestValues.Ticket2_Number);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
            yield return new TicketAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number);
            yield return new CustomerTaken(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name, CustomerQueueTestValues.Ticket1_Id);
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void customer_taken_is_not_produced() => ProducedEvents.Should().NotContain(
            new CustomerTaken(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name, CustomerQueueTestValues.Ticket2_Id));
    }
}