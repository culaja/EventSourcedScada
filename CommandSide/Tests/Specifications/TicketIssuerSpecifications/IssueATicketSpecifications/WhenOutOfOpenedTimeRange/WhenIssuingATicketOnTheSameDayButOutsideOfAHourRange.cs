using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Shared.TicketIssuer.Events;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.IssueATicketSpecifications.WhenOutOfOpenedTimeRange
{
    public sealed class WhenIssuingATicketOnTheSameDayButOutsideOfAHourRange : TicketIssuerSpecification<IssueATicket>
    {
        public WhenIssuingATicketOnTheSameDayButOutsideOfAHourRange() : base(SingleTicketIssuerId)
        {
        }

        protected override IssueATicket CommandToExecute => new IssueATicket(TicketNumber1);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            yield return Monday14To16Added;
        }

        public override CommandHandler<IssueATicket> When() => new IssueATicketHandler(
            TicketIssuerRepository,
            AlwaysMonday10LocalTimeProviderStub,
            Ticket2IdGenerator);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void Ticket1_is_issued() => ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<TicketIssued>());
    }
}

    