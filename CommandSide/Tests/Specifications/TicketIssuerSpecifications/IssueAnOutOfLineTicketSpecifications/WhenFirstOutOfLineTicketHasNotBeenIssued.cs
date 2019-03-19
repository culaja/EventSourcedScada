using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.IssueAnOutOfLineTicketSpecifications
{
    public sealed class WhenFirstOutOfLineTicketHasNotBeenIssued : TicketIssuerSpecification<IssueAnOutOfLineTicket>
    {
        public WhenFirstOutOfLineTicketHasNotBeenIssued() : base(SingleTicketIssuerId)
        {
        }

        protected override IssueAnOutOfLineTicket CommandToExecute => new IssueAnOutOfLineTicket(Counter1Id);
        
        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
        }

        public override CommandHandler<IssueAnOutOfLineTicket> When() => new IssueAnOutOfLineTicketHandler(
            TicketIssuerRepository,
            Ticket10kIdGenerator);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produced_out_of_line_ticket_issued_event_for_ticket_10k() =>
            ProducedEvents.Should().Contain(OutOfLineTicket10kIssuedForCounter1);

    }
}