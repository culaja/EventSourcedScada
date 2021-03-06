using System;
using CommandSide.Domain.RemoteDomain;
using Shared.Remote.Events;
using static CommandSide.Domain.RemoteDomain.PointCoordinate;
using static CommandSide.Domain.RemoteDomain.PointName;
using static CommandSide.Domain.RemoteDomain.RemoteName;

namespace CommandSide.Tests.Specifications.RemoteSpecifications
{
    public static class RemoteTestValues
    {
        public static readonly Guid Remote1Id = new Guid();
        public static readonly RemoteName Remote1Name = RemoteNameFrom("remote1");
        
        public static RemoteCreated Remote1Created => new RemoteCreated(Remote1Id, Remote1Name);
        
        public static readonly PointName Analog1Name = PointNameFrom("analog1");
        public static readonly PointCoordinate Analog1Coordinate = PointCoordinateFrom(1);
        public static readonly PointCoordinate NewAnalog1Coordinate = PointCoordinateFrom(1001);
        public static AnalogAdded Analog1Added => new AnalogAdded(Remote1Id, Analog1Name, Analog1Coordinate);
        public static AnalogCoordinateUpdated Analog1CoordinateUpdated => new AnalogCoordinateUpdated(Remote1Id, Analog1Name, Analog1Coordinate, NewAnalog1Coordinate);
        
        public static readonly PointName Analog2Name = PointNameFrom("analog2");
        public static readonly PointCoordinate Analog2Coordinate = PointCoordinateFrom(2);
        public static readonly PointCoordinate NewAnalog2Coordinate = PointCoordinateFrom(1002);
        public static AnalogAdded Analog2Added => new AnalogAdded(Remote1Id, Analog2Name, Analog2Coordinate);
    }
}