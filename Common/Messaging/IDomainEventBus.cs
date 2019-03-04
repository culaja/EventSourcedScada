using System.Collections.Generic;

namespace Common.Messaging
{
    public interface IDomainEventBus
    {
        IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages);

        IMessage Dispatch(IMessage message);
    }
}