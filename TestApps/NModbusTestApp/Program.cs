using System.Collections.Generic;
using CommandSide.Domain.RemoteDomain;
using Common;
using NModbusAdapter;
using static System.Console;
using static Common.Nothing;

namespace NModbusTestApp
{
    class Program
    {
        static void Main()
        {
            new AnalogSynchronousReader().BulkReadFor(new List<PointCoordinate>())
                .Map(av => ToNothing(() => WriteLine(av)));
        }
    }
}