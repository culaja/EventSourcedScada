using System;
using Common.Messaging;

namespace CommandSide.Tests.IntegrationTests.EventStore
{
    public static class DomainEventTestExtensions
    {
        public static IDomainEvent SetAnyVersionAndTimestamp(this DomainEvent e) => e
            .SetVersion(4)
            .SetTimestamp(DateTime.Now);
    }
}