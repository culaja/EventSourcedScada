using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Shared.Remote.Events;
using Xunit;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.AddAnalogSpecifications
{
    public sealed class WhenAddingSecondAnalogPoint : RemoteSpecification<AddAnalog>
    {
        public WhenAddingSecondAnalogPoint() : base(Remote1Id)
        {
        }

        protected override AddAnalog CommandToExecute => new AddAnalog(Remote1Id, Analog2Name, Analog2Coordinate);
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Remote1Created;
            yield return Analog1Added;
        }

        public override CommandHandler<AddAnalog> When() => new AddAnalogHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produces_analog2_added() => ProducedEvents.Should().Contain(Analog2Added);
    }
}