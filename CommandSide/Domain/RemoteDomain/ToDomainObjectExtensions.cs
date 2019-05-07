using static CommandSide.Domain.RemoteDomain.PointCoordinate;
using static CommandSide.Domain.RemoteDomain.PointName;

namespace CommandSide.Domain.RemoteDomain
{
    internal static class ToDomainObjectExtensions
    {
        public static PointName ToPointName(this string pointName) => PointNameFrom(pointName);

        public static PointCoordinate ToPointCoordinate(this int pointCoordinate) => PointCoordinateFrom(pointCoordinate);
    }
}