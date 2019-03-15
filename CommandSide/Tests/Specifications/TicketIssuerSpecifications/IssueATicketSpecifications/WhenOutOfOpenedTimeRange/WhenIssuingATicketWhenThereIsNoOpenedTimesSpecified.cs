using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.IssueATicketSpecifications.WhenOutOfOpenedTimeRange
{
    public sealed class WhenIssuingATicketWhenThereIsNoOpenedTimesSpecified : TicketIssuerSpecification<IssueATicket>
    {
        public WhenIssuingATicketWhenThereIsNoOpenedTimesSpecified() : base(SingleTicketIssuerId)
        {
        }

        protected override IssueATicket CommandToExecute => new IssueATicket(TicketNumber1);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
        }

        public override CommandHandler<IssueATicket> When() => new IssueATicketHandler(
            TicketIssuerRepository,
            AlwaysMonday10UtcTimeProviderStub,
            Ticket2IdGenerator);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void Ticket1_is_issued() => ProducedEvents.Should().NotContain(AssertionsHelpers.EventOf<TicketIssued>());
    }
}