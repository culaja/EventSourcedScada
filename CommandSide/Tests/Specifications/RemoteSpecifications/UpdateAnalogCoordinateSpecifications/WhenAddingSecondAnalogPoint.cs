using System;
using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain.Commands;
using CommandSide.DomainServices.RemoteHandlers.CommandHandlers;
using Common.Messaging;
using FluentAssertions;
using Shared.Remote;
using Xunit;
using static CommandSide.Tests.Specifications.RemoteSpecifications.RemoteTestValues;

namespace CommandSide.Tests.Specifications.RemoteSpecifications.UpdateAnalogCoordinateSpecifications
{
    public sealed class WhenPassingDifferentCoordinate : RemoteSpecification<UpdateAnalogCoordinate>
    {
        public WhenPassingDifferentCoordinate() : base(Remote1Id)
        {
        }

        protected override UpdateAnalogCoordinate CommandToExecute => new UpdateAnalogCoordinate(Remote1Id, Analog1Name, NewAnalog1Coordinate);
        
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Remote1Created;
            yield return Analog1Added;
        }

        public override CommandHandler<UpdateAnalogCoordinate> When() => new UpdateAnalogCoordinateHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void produces_analog_point_coordinate_changed() => ProducedEvents.Should().Contain(Analog1CoordinateUpdated);
    }
}