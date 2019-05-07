using System;
using CommandSide.Domain.RemoteDomain;
using Shared.Remote.Events;
using static CommandSide.Domain.RemoteDomain.PointCoordinate;
using static CommandSide.Domain.RemoteDomain.PointName;

namespace CommandSide.Tests.Specifications.RemoteSpecifications
{
    public static class RemoteTestValues
    {
        public static readonly Guid Remote1Id = new Guid();
        
        public static readonly RemoteCreated Remote1Created = new RemoteCreated(Remote1Id);
        
        public static readonly PointName Analog1Name = PointNameFrom("analog1");
        public static readonly PointCoordinate Analog1Coordinate = PointCoordinateFrom(1);
        public static readonly AnalogAdded Analog1Added = new AnalogAdded(Remote1Id, Analog1Name, Analog1Coordinate);
        
        public static readonly PointName Analog2Name = PointNameFrom("analog2");
        public static readonly PointCoordinate Analog2Coordinate = PointCoordinateFrom(2);
        public static readonly AnalogAdded Analog2Added = new AnalogAdded(Remote1Id, Analog2Name, Analog2Coordinate);
    }
}