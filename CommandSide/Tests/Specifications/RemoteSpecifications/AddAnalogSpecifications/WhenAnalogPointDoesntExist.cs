using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Xunit;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.AddAnalogSpecifications
{
    public sealed class WhenAnalogPointDoesntExist : RemoteSpecification<AddAnalog>
    {
        protected override AddAnalog CommandToExecute => new AddAnalog(Remote1Id, Analog1Name, Analog1Coordinate);
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Apply(Remote1Created);
        }

        public override CommandHandler<AddAnalog> When() => new AddAnalogHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produces_analog1_added() => ProducedEvents.Should().Contain(Analog1Added);
    }
}