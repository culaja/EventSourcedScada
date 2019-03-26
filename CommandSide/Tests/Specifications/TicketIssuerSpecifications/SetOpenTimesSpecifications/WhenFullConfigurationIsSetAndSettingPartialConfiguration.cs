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
    public sealed class WhenFullConfigurationIsSetAndSettingPartialConfiguration : TicketIssuerSpecification<SetOpenTimes>
    {
        public WhenFullConfigurationIsSetAndSettingPartialConfiguration() : base(SingleTicketIssuerId)
        {
        }

        protected override SetOpenTimes CommandToExecute => new SetOpenTimes(MondayOpenTimes);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            foreach (var item in AllOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetOpenTimes> When() => new SetOpenTimesHandler(TicketIssuerRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void Tuesday9To12_is_removed() => ProducedEvents.Should().Contain(Tuesday9To12Removed);

        [Fact]
        public void Monday9To12_is_not_removed() => ProducedEvents.Should().NotContain(Monday9To12Removed);

        [Fact]
        public void Monday14To16_is_not_removed() => ProducedEvents.Should().NotContain(Monday14To16Removed);
    }
}