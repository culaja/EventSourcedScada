using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace CommonAdapters.AutofacMessageBus
{
    internal sealed class MessageContext
    {
        public IMessage MessageToHandle { get; }
        private readonly TaskCompletionSource<Result> _taskCompletionSource = new TaskCompletionSource<Result>();
		

        public MessageContext(IMessage messageToHandle)
        {
            MessageToHandle = messageToHandle;
        }

        public Result FinalizeWith(Result result)
        {
            _taskCompletionSource.SetResult(result);
            return result;
        }

        public Task<Result> WaitToBeHandledAsync() => _taskCompletionSource.Task;
    }
}