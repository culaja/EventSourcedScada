using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.SetOpenTimesSpecifications
{
    public sealed class WhenThereIsNoConfigurationSetAndSettingFullConfiguration : TicketIssuerSpecification<SetOpenTimes>
    {
        public WhenThereIsNoConfigurationSetAndSettingFullConfiguration() : base(SingleTicketIssuerId)
        {
        }

        protected override SetOpenTimes CommandToExecute => new SetOpenTimes(AllOpenTimes);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
        }

        public override CommandHandler<SetOpenTimes> When() => new SetOpenTimesHandler(TicketIssuerRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllOpenTimes_are_added() => ProducedEvents.Should().ContainInOrder(AllOpenTimesAdded);
    }
}