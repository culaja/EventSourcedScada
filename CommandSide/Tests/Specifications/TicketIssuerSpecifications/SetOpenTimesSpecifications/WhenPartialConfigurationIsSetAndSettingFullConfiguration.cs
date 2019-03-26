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
    public sealed class WhenPartialConfigurationIsSetAndSettingFullConfiguration : TicketIssuerSpecification<SetOpenTimes>
    {
        public WhenPartialConfigurationIsSetAndSettingFullConfiguration() : base(SingleTicketIssuerId)
        {
        }

        protected override SetOpenTimes CommandToExecute => new SetOpenTimes(AllOpenTimes);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            foreach (var item in MondayOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetOpenTimes> When() => new SetOpenTimesHandler(TicketIssuerRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Tuesday9to12_is_added() => ProducedEvents.Should().Contain(Tuesday9To12Added);

        [Fact]
        public void Monday9to12_is_not_added() => ProducedEvents.Should().NotContain(Monday9To12Added);

        [Fact]
        public void Monday14to16_is_not_added() => ProducedEvents.Should().NotContain(Monday14To16Added);
    }
}