using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Messaging
{
    public interface ILocalMessageBus
    {
        IReadOnlyList<IMessage> DispatchAll(IReadOnlyList<IMessage> messages);

        IMessage Dispatch(IMessage message);

        Task<Result> HandleAsync(IMessage message);
    }
}