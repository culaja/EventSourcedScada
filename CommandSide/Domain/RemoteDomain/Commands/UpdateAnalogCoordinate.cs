using System;
using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class UpdateAnalogCoordinate : ICommand
    {
        public Guid RemoteId { get; }
        public PointName PointName { get; }
        public PointCoordinate NewPointCoordinate { get; }

        public UpdateAnalogCoordinate(
            Guid remoteId,
            PointName pointName,
            PointCoordinate newPointCoordinate)
        {
            RemoteId = remoteId;
            PointName = pointName;
            NewPointCoordinate = newPointCoordinate;
        }
    }
}