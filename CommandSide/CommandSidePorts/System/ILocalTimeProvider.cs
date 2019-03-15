using System;

namespace CommandSide.CommandSidePorts.System
{
    public interface ILocalTimeProvider
    {
        DateTime CurrentTime { get; }
    }
}