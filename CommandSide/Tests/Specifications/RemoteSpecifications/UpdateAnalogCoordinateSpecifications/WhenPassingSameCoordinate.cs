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

namespace CommandSide.Tests.Specifications.RemoteSpecifications.UpdateAnalogCoordinateSpecifications
{
    public sealed class WhenPassingSameCoordinate : RemoteSpecification<UpdateAnalogCoordinate>
    {
        protected override UpdateAnalogCoordinate CommandToExecute => new UpdateAnalogCoordinate(Remote1Id, Analog1Name, NewAnalog1Coordinate);
        
        public override IEnumerable<RemoteEvent> Given()
        {
            yield return Apply(Remote1Created);
            yield return Apply(Analog1Added);
            yield return Apply(Analog1CoordinateUpdated);
        }

        public override CommandHandler<UpdateAnalogCoordinate> When() => new UpdateAnalogCoordinateHandler(RemoteRepository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void doesnt_produce_analog_point_coordinate_changed() => 
            ProducedEvents.Should().NotContain(EventOf<AnalogCoordinateUpdated>());
    }
}