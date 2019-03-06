using System.Threading;
using System.Threading.Tasks;

namespace QuerySide.QueryCommon
{
    // https://blogs.msdn.microsoft.com/pfxteam/2012/02/11/building-async-coordination-primitives-part-1-asyncmanualresetevent/
    internal class AsyncManualResetEvent
    {
        private volatile TaskCompletionSource<bool> _taskCompletionSource = new TaskCompletionSource<bool>();
 
        public Task WaitAsync() => _taskCompletionSource.Task;
 
        public void Set()
        {
            // https://stackoverflow.com/questions/12693046/configuring-the-continuation-behaviour-of-a-taskcompletionsources-task
            Task.Run(() =>
                _taskCompletionSource.TrySetResult(true));
        }
 
        public void Reset()
        {
            while (true)
            {
                var tcs = _taskCompletionSource;
                if (!tcs.Task.IsCompleted ||
                    Interlocked.CompareExchange(ref _taskCompletionSource, new TaskCompletionSource<bool>(), tcs) == tcs)
                {
                    return;
                }
            }
        }
    }
}