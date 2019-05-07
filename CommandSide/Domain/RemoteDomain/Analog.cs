using System;
using Common;
using Shared.Remote.Events;

namespace CommandSide.Domain.RemoteDomain
{
    internal sealed class Analog : Entity<PointName>
    {
        private readonly Guid _remoteId;
        private PointCoordinate _pointCoordinate;

        private Analog(
            Guid remoteId,
            PointName pointName,
            PointCoordinate pointCoordinate) : base(pointName)
        {
            _remoteId = remoteId;
            _pointCoordinate = pointCoordinate;
        }
        
        public static Analog AnalogFrom(
            Guid remoteId,
            PointName pointName,
            PointCoordinate pointCoordinate) =>
            new Analog(remoteId, pointName, pointCoordinate);
        
        public Maybe<AnalogCoordinateUpdated> GenerateAnalogCoordinateUpdatedWith(PointCoordinate newPointCoordinate)
            => _pointCoordinate.Equals(newPointCoordinate).OnBoth(
                () => Maybe<AnalogCoordinateUpdated>.None, 
                () => new AnalogCoordinateUpdated(_remoteId, Id, _pointCoordinate, newPointCoordinate));

        public void Update(PointCoordinate newPointCoordinate)
        {
            _pointCoordinate = newPointCoordinate;
        }
    }
}