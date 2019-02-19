using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.Specifications.CustomerQueueSpecifications.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenTicketWithSameNumberExists : CustomerQueueSpecification<AddTicket>
    {
        protected override AddTicket CommandToExecute => new AddTicket(Ticket2_Id, Ticket1_Number, Ticket2_PrintingTimestamp);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new TicketAdded(AggregateRootId, Ticket1_Id, Ticket1_Number, Ticket1_PrintingTimestamp);
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void ticket_added_event_is_not_produced() => ProducedEvents.Should().NotContain(new TicketAdded(
            AggregateRootId,
            Ticket2_Id,
            Ticket1_Number,
            Ticket2_PrintingTimestamp));

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}