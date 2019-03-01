namespace Common.Messaging
{
    public abstract class EventHandler<T> : MessageHandler<T> where T : IDomainEvent
    {
    }
}