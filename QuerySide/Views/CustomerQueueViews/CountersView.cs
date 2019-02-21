using System;
using System.Collections.Generic;
using System.Text;
using QueryCommon;
using Shared.CustomerQueue;

namespace CustomerQueueViews
{
    internal sealed class CountersView : View,
        IHandle<CounterAdded>,
        IHandle<TicketAdded>,
        IHandle<CustomerTaken>,
        IHandle<CustomerServed>,
        IHandle<CustomerRevoked>
    {
        private readonly Dictionary<string, int> _counterDictionary = new Dictionary<string, int>();
        private readonly Dictionary<Guid, int> _ticketDictionary = new Dictionary<Guid, int>();

        public void Handle(CounterAdded e)
        {
            _counterDictionary.Add(e.CounterName, 0);
        }

        public void Handle(TicketAdded e)
        {
            _ticketDictionary.Add(e.TicketId, e.TicketNumber);
        }

        public void Handle(CustomerTaken e)
        {
            _counterDictionary[e.CounterName] = _ticketDictionary[e.TicketId];
        }

        public void Handle(CustomerServed e)
        {
            _counterDictionary[e.CounterName] = 0;
        }

        public void Handle(CustomerRevoked e)
        {
            _counterDictionary[e.CounterName] = 0;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Total counters: {_counterDictionary.Count}");
            builder.AppendLine($"Total tickets: {_ticketDictionary.Count}");
            builder.AppendLine("***************************************************************************");
            foreach (var kv in _counterDictionary) builder.AppendLine($"{kv.Key}:\t\t {kv.Value}");
            builder.AppendLine("***************************************************************************");
            builder.AppendLine();
            return builder.ToString();
        }
    }
}