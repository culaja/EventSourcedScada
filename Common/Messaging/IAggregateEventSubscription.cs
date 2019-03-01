namespace Common.Messaging
{
    public interface IAggregateEventSubscription
    {
        string AggregateTopicName { get; }
    }
}