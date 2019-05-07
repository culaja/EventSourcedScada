using System;

namespace Shared.Remote.Events
{
    public sealed class AnalogAdded : RemoteEvent
    {
        public string PointName { get; }
        public int PointCoordinate { get; }

        public AnalogAdded(
            Guid aggregateRootId,
            string pointName,
            int pointCoordinate) : base(aggregateRootId)
        {
            PointName = pointName;
            PointCoordinate = pointCoordinate;
        }
    }
}