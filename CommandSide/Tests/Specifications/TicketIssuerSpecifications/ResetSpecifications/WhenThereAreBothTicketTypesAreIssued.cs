using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.ResetSpecifications
{
    public sealed class WhenThereAreBothTicketTypesAreIssued : TicketIssuerSpecification<ResetTicketIssuer>
    {
        public WhenThereAreBothTicketTypesAreIssued() : base(SingleTicketIssuerId)
        {
        }

        protected override ResetTicketIssuer CommandToExecute => new ResetTicketIssuer();

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            yield return Ticket1Issued;
            yield return Ticket2Issued;
            yield return OutOfLineTicket10kIssuedForCounter1;
        }

        public override CommandHandler<ResetTicketIssuer> When() => new ResetTicketIssuerHandler(TicketIssuerRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void ticket_issuer_reset_with_last_ticket_numbers_for_both_types() =>
            ProducedEvents.Should().Contain(TicketIssuerHasResetWithLastTicketNumbers(3, 10001));
    }
}