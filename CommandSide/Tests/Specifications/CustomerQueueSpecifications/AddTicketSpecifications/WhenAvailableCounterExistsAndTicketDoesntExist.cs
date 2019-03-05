using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenAvailableCounterExistsAndTicketDoesntExist : CustomerQueueSpecification<AddTicket>
    {
        public WhenAvailableCounterExistsAndTicketDoesntExist() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name);
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void after_ticked_added_also_customer_taken_is_produced() => ProducedEvents.Should().BeEquivalentTo(
            new TicketAdded(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number),
            new CustomerTaken(CustomerQueueTestValues.SingleCustomerQueueId, CustomerQueueTestValues.CounterA_Name, CustomerQueueTestValues.Ticket1_Id));
    }
}