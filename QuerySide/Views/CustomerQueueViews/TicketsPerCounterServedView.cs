using System.Collections.Generic;
using System.Text;
using QueryCommon;
using Shared.CustomerQueue;

namespace CustomerQueueViews
{
    public sealed class TicketsPerCounterServedView : View,
        IHandle<CustomerServed>
    {
        private readonly  Dictionary<string, int> _dictionary = new Dictionary<string, int>();
        
        public void Handle(CustomerServed e)
        {
            if (!_dictionary.TryGetValue(e.CounterName, out var tickets))
            {
                _dictionary[e.CounterName] = 0;
            }

            _dictionary[e.CounterName] = tickets + 1;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("*************************Tickets per counter ******************************");
            foreach (var kv in _dictionary) builder.AppendLine($"{kv.Key}:\t\t {kv.Value}");
            builder.AppendLine("***************************************************************************");
            builder.AppendLine();
            return builder.ToString();
        }
    }
}