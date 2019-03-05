using System.Collections.Generic;
using System.Text;
using QuerySide.QueryCommon;
using Shared.CustomerQueue;

namespace QuerySide.Views.CustomerQueueViews
{
    internal sealed class TicketsPerCounterView : View,
        IHandle<CounterAdded>,
        IHandle<CustomerServed>,
        IHandle<CustomerRevoked>
    {
        private readonly  Dictionary<string, TicketDetails> _dictionary = new Dictionary<string, TicketDetails>();

        public IReadOnlyDictionary<string, TicketDetails> CountersDetails => _dictionary;
        
        public void Handle(CustomerServed e) => TicketDetailsFrom(e.CounterName).IncrementServedTickets();

        public void Handle(CustomerRevoked e) => TicketDetailsFrom(e.CounterName).IncrementRevokedTickets();

        public void Handle(CounterAdded e) => TicketDetailsFrom(e.CounterName);

        private TicketDetails TicketDetailsFrom(string counterName)
        {
            if (!_dictionary.TryGetValue(counterName, out var ticketDetails))
            {
                ticketDetails = new TicketDetails();
                _dictionary[counterName] = ticketDetails;
            }

            return ticketDetails;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("*************************Tickets per counter ******************************");
            builder.AppendLine(TicketDetails.FormatColumns);
            foreach (var kv in _dictionary) builder.AppendLine($"{kv.Key}: {kv.Value}");
            builder.AppendLine("***************************************************************************");
            builder.AppendLine();
            return builder.ToString();
        }

        public sealed class TicketDetails
        {
            public const string FormatColumns = "\t\tServed\t\t\tRevoked";
            
            public int ServedTickets { get; private set; }
            public int RevokedTickets { get; private set; }

            public void IncrementServedTickets() => ServedTickets++;
            public void IncrementRevokedTickets() => RevokedTickets++;

            public override string ToString() => $"\t\t{ServedTickets}\t\t\t{RevokedTickets}";
        }
    }
}