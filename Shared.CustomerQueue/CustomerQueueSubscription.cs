using Common.Messaging;

namespace Shared.CustomerQueue
{
    public sealed class CustomerQueueSubscription : IAggregateEventSubscription
    {
        public string AggregateTopicName => "CustomerQueue";
    }
}