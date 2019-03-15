using System;
using static System.DateTime;

namespace CommandSide.CommandSidePorts.System
{
    public sealed class StandardLocalTimeProvider : ILocalTimeProvider
    {
        public DateTime CurrentTime => Now.ToLocalTime();
    }
}