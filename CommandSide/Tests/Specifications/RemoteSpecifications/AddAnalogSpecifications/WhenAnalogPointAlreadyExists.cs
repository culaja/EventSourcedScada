using System;
using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Shared.Remote.Events;
using Xunit;
using static CommandSide.Tests.AssertionsHelpers;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.AddAnalogSpecifications
{
    public sealed class WhenAnalogPointAlreadyExists : RemoteSpecification<AddAnalog>
    {
        public WhenAnalogPointAlreadyExists() : base(Remote1Id)
        {
        }

        protected override AddAnalog CommandToExecute => new AddAnalog(Remote1Id, Analog1Name, Analog1Coordinate);
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Remote1Created;
            yield return Analog1Added;
        }

        public override CommandHandler<AddAnalog> When() => new AddAnalogHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeFalse();

        [Fact]
        public void doesnt_produce_analog_added() =>
            ProducedEvents.Should().NotContain(EventOf<AnalogAdded>());
    }
}