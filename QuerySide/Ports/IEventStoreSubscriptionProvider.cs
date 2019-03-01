using Common.Messaging;

namespace Ports
{
    public interface IEventStoreSubscriptionProvider
    {
        IEventStoreSubscription<T> MakeSubscriptionFor<T>() where T : IAggregateEventSubscription, new();
    }
}