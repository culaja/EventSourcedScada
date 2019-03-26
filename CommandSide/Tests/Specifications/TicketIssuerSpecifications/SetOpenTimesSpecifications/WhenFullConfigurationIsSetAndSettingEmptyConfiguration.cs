using System.Collections.Generic;
using CommandSide.Domain.TicketIssuing.Commands;
using CommandSide.DomainServices.TicketIssuing.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.TicketIssuer;
using Xunit;
using static CommandSide.Domain.TicketIssuing.OpenTimes;
using static CommandSide.Tests.Specifications.TicketIssuerSpecifications.TicketIssuerTestValues;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.SetOpenTimesSpecifications
{
    public sealed class WhenFullConfigurationIsSetAndSettingEmptyConfiguration : TicketIssuerSpecification<SetOpenTimes>
    {
        public WhenFullConfigurationIsSetAndSettingEmptyConfiguration() : base(SingleTicketIssuerId)
        {
        }

        protected override SetOpenTimes CommandToExecute => new SetOpenTimes(NoOpenTimes);

        public override IEnumerable<TicketIssuerEvent> Given()
        {
            yield return SingleTicketIssuerCreated;
            foreach (var item in AllOpenTimesAdded) yield return item;
        }

        public override CommandHandler<SetOpenTimes> When() => new SetOpenTimesHandler(TicketIssuerRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void AllOpenTimes_are_removed() => ProducedEvents.Should().ContainInOrder(AllOpenTimesRemoved);
    }
}