using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using static Common.Result;

namespace Tests.IntegrationTests
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