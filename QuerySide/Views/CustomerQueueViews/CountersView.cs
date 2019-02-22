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
        private readonly Dictionary<string, string> _counterDictionary = new Dictionary<string, string>();
        private readonly Dictionary<Guid, string> _ticketDictionary = new Dictionary<Guid, string>();

        public IReadOnlyDictionary<string, string> CountersState => _counterDictionary;

        public void Handle(CounterAdded e)
        {
            _counterDictionary.Add(e.CounterName, "-");
        }

        public void Handle(TicketAdded e)
        {
            _ticketDictionary.Add(e.TicketId, e.TicketNumber == 0 ? "-" : e.TicketNumber.ToString());
        }

        public void Handle(CustomerTaken e)
        {
            _counterDictionary[e.CounterName] = _ticketDictionary[e.TicketId];
        }

        public void Handle(CustomerServed e)
        {
            _counterDictionary[e.CounterName] = "-";
        }

        public void Handle(CustomerRevoked e)
        {
            _counterDictionary[e.CounterName] = "-";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("*************************Counters state ************************************");
            builder.AppendLine($"Total counters: {_counterDictionary.Count} \t\t\t\t\t Total tickets: {_ticketDictionary.Count}");
            builder.AppendLine("\t\tServing ticket number");
            foreach (var kv in _counterDictionary) builder.AppendLine($"{kv.Key}:\t\t {kv.Value}");
            builder.AppendLine("***************************************************************************");
            builder.AppendLine();
            return builder.ToString();
        }
    }
}