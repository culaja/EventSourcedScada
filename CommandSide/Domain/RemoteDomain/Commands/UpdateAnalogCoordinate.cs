using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class UpdateAnalogCoordinate : ICommand
    {
        public RemoteName RemoteName { get; }
        public PointName PointName { get; }
        public PointCoordinate NewPointCoordinate { get; }

        public UpdateAnalogCoordinate(
            RemoteName remoteName,
            PointName pointName,
            PointCoordinate newPointCoordinate)
        {
            RemoteName = remoteName;
            PointName = pointName;
            NewPointCoordinate = newPointCoordinate;
        }
    }
}