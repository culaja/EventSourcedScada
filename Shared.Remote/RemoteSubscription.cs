using Common.Messaging;

namespace Shared.Remote
{
    public sealed class RemoteSubscription : IAggregateEventSubscription
    {
        public string AggregateTopicName => "Remote";
    }
}