using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenAvailableCounterExistsAndTicketDoesntExist : CustomerQueueSpecification<AddTicket>
    {
        public WhenAvailableCounterExistsAndTicketDoesntExist() : base(SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void after_ticked_added_also_customer_taken_is_produced() => ProducedEvents.Should().BeEquivalentTo(
            new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp),
            new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id, Ticket1_PrintingTimestamp));
    }
}