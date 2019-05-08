using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using CommandSide.CommandSidePorts.Telemetry;
using CommandSide.Domain;
using CommandSide.Domain.RemoteDomain;
using Common;
using NModbus;

namespace NModbusAdapter
{
    public class AnalogSynchronousReader : IAnalogSynchronousReader
    {
        public IReadOnlyList<AnalogValue> BulkReadFor(IReadOnlyList<PointCoordinate> pointCoordinates)
        {
            byte slaveId = 1;
            int port = 5003;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });
            
            var factory = new ModbusFactory();
            
            // create the master
            TcpClient masterTcpClient = new TcpClient(address.ToString(), port);
            IModbusMaster master = factory.CreateMaster(masterTcpClient);

            ushort[] inputs = master.ReadHoldingRegisters(1, 0, 10);

            return inputs.Map(i => AnalogValue.AnalogValueFrom(i));
        }
    }
}
