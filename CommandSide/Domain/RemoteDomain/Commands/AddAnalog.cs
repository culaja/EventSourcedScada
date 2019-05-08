using Common.Messaging;

namespace CommandSide.Domain.RemoteDomain.Commands
{
    public sealed class AddAnalog : ICommand
    {
        public RemoteName RemoteName { get; }
        public PointName PointName { get; }
        public PointCoordinate PointCoordinate { get; }

        public AddAnalog(
            RemoteName remoteName,
            PointName pointName, 
            PointCoordinate pointCoordinate)
        {
            RemoteName = remoteName;
            PointName = pointName;
            PointCoordinate = pointCoordinate;
        }
    }
}