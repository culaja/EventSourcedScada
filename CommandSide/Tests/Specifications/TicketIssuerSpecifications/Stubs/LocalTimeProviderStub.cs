using System;
using CommandSide.CommandSidePorts.System;

namespace CommandSide.Tests.Specifications.TicketIssuerSpecifications.Stubs
{
    public sealed class LocalTimeProviderStub : ILocalTimeProvider
    {
        public LocalTimeProviderStub(DateTime currentTime)
        {
            CurrentTime = currentTime.ToLocalTime();
        }

        public DateTime CurrentTime { get; }
    }
}