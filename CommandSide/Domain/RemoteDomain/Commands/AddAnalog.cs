using System;
using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class AddAnalog : ICommand
    {
        public Guid RemoteId { get; }
        public PointName PointName { get; }
        public PointCoordinate PointCoordinate { get; }

        public AddAnalog(
            Guid remoteId,
            PointName pointName, 
            PointCoordinate pointCoordinate)
        {
            RemoteId = remoteId;
            PointName = pointName;
            PointCoordinate = pointCoordinate;
        }
    }
}