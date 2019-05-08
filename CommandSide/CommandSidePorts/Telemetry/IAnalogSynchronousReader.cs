using System.Collections.Generic;
using CommandSide.Domain;
using CommandSide.Domain.RemoteDomain;

namespace CommandSide.CommandSidePorts.Telemetry
{
    public interface IAnalogSynchronousReader
    {
        IReadOnlyList<AnalogValue> BulkReadFor(IReadOnlyList<PointCoordinate> pointCoordinates);
    }
}