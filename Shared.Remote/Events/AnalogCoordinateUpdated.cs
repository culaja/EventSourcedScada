using System;

namespace Shared.Remote.Events
{
    public sealed class AnalogCoordinateUpdated : RemoteEvent
    {
        public string PointName { get; }
        public int OldPointCoordinate { get; }
        public int NewPointCoordinate { get; }

        public AnalogCoordinateUpdated(
            Guid aggregateRootId,
            string pointName,
            int oldPointCoordinate,
            int newPointCoordinate) : base(aggregateRootId)
        {
            PointName = pointName;
            OldPointCoordinate = oldPointCoordinate;
            NewPointCoordinate = newPointCoordinate;
        }
    }
}