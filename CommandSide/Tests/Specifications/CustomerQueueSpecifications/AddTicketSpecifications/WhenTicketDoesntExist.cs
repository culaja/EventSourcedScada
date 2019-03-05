using System.Collections.Generic;
using CommandSide.Domain.Commands;
using CommandSide.DomainServices.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;

namespace CommandSide.Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenTicketDoesntExist : CustomerQueueSpecification<AddTicket>
    {
        public WhenTicketDoesntExist() : base(CustomerQueueTestValues.SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(CustomerQueueTestValues.Ticket1_Id, CustomerQueueTestValues.Ticket1_Number);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void ticket_added_event_exists() => ProducedEvents.Should().Contain(new TicketAdded(
            CustomerQueueTestValues.SingleCustomerQueueId,
            CustomerQueueTestValues.Ticket1_Id,
            CustomerQueueTestValues.Ticket1_Number));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}