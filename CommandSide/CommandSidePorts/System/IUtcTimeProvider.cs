using System;

namespace CommandSide.CommandSidePorts.System
{
    public interface IUtcTimeProvider
    {
        DateTime CurrentTime { get; }
    }
}