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
    public sealed class WhenTicket1HasBeenIssuedAndIssuingTicket2 : TicketIssuerSpecification<IssueATicket>
    {
        public WhenTicket1HasBeenIssuedAndIssuingTicket2() : base(SingleTicketIssuerId)
        {
        }

        protected override IssueATicket CommandToExecute => new IssueATicket(TicketNumber2);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            yield return Monday9To12Added;
            yield return Ticket1Issued;
        }

        public override CommandHandler<IssueATicket> When() => new IssueATicketHandler(
            TicketIssuerRepository,
            AlwaysMonday10LocalTimeProviderStub,
            Ticket2IdGenerator);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Ticket1_is_issued() => ProducedEvents.Should().Contain(Ticket2Issued);
    }
}