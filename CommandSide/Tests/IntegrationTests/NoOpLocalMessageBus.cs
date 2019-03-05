using System.Collections.Generic;
using Common.Messaging;

namespace CommandSide.Tests.IntegrationTests
{
    public sealed class NoOpDomainEventBus : IDomainEventBus
    {
        public IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages)
        {
            return messages;
        }

        public IMessage Dispatch(IMessage message)
        {
            return message;
        }
    }
}