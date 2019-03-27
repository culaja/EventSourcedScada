using System.Collections.Generic;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;

namespace QuerySide.Views
{
    public sealed class CountersAddedView : SynchronizedView,
        IHandle<CounterAdded>
    {
        public List<string> Counters { get; } = new List<string>();
        
        public void Handle(CounterAdded e)
        {
            Counters.Add(e.CounterId);
        }
    }
}