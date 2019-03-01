using System.Collections.Generic;
using Common.Messaging;

namespace AutofacMessageBus
{
    public sealed class MessageHandlerComparer: IEqualityComparer<IMessageHandler>
    {
        public bool Equals(IMessageHandler x, IMessageHandler y) =>
            x.GetType() == y.GetType();

        public int GetHashCode(IMessageHandler obj) => obj.GetType().GetHashCode();
    }
}