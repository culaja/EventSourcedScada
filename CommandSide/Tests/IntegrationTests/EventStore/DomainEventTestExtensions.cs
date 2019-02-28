using Common.Messaging;
using static System.DateTime;

namespace Tests.IntegrationTests.EventStore
{
    public static class DomainEventTestExtensions
    {
        public static IDomainEvent SetAnyVersionAndTimestamp(this DomainEvent e) => e
            .SetVersion(4)
            .SetTimestamp(Now);
    }
}