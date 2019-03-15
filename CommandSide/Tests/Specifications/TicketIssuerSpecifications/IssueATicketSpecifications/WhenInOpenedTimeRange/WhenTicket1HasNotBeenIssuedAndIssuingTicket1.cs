using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.IssueATicketSpecifications.WhenInOpenedTimeRange
{
    public sealed class WhenTicket1HasNotBeenIssuedAndIssuingTicket1 : TicketIssuerSpecification<IssueATicket>
    {
        public WhenTicket1HasNotBeenIssuedAndIssuingTicket1() : base(SingleTicketIssuerId)
        {
        }

        protected override IssueATicket CommandToExecute => new IssueATicket(TicketNumber1);
        
        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            yield return Monday9To12Added;
        }

        public override CommandHandler<IssueATicket> When() => new IssueATicketHandler(
            TicketIssuerRepository,
            AlwaysMonday10UtcTimeProviderStub,
            Ticket1IdGenerator);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Ticket1_is_issued() => ProducedEvents.Should().Contain(Ticket1Issued);
    }
}