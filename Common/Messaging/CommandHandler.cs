namespace Common.Messaging
{
    public abstract class CommandHandler<T> : MessageHandler<T> where T : ICommand
    {
    }
}