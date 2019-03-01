using Common;
using Common.Messaging;

namespace Ports
{
    public delegate Nothing EventStoreSubscriptionHandler(IDomainEvent e);
}