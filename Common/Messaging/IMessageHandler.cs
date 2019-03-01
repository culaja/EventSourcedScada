namespace Common.Messaging
{
    public interface IMessageHandler<in T>
        where T : IMessage
    {
        Result Handle(T message);
    }

    public interface IMessageHandler
    {
        Result Handle(IMessage @message);
    }
}