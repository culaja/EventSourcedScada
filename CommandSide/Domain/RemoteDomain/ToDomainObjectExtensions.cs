using static CommandSide.Domain.RemoteDomain.PointCoordinate;
using static CommandSide.Domain.RemoteDomain.PointName;
using static CommandSide.Domain.RemoteDomain.RemoteName;

namespace CommandSide.Domain.RemoteDomain
{
    public static class ToDomainObjectExtensions
    {
        public static RemoteName ToRemoteName(this string remoteName) => RemoteNameFrom(remoteName);
        
        public static PointName ToPointName(this string pointName) => PointNameFrom(pointName);

        public static PointCoordinate ToPointCoordinate(this int pointCoordinate) => PointCoordinateFrom(pointCoordinate);
    }
}