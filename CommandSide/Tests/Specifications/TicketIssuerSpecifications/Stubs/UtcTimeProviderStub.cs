using System;
using CommandSide.CommandSidePorts.System;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.Stubs
{
    public sealed class UtcTimeProviderStub : IUtcTimeProvider
    {
        public UtcTimeProviderStub(DateTime currentTime)
        {
            CurrentTime = currentTime.ToUniversalTime();
        }

        public DateTime CurrentTime { get; }
    }
}