using System.Collections.Generic;
using System.Text;
using QueryCommon;
using Shared.CustomerQueue;

namespace CustomerQueueViews
{
    internal sealed class TicketsPerCounterView : View,
        IHandle<CustomerServed>,
        IHandle<CustomerRevoked>
    {
        private readonly  Dictionary<string, TicketDetails> _dictionary = new Dictionary<string, TicketDetails>();
        
        public void Handle(CustomerServed e) => TicketDetailsFrom(e.CounterName).IncrementServedTickets();

        public void Handle(CustomerRevoked e) => TicketDetailsFrom(e.CounterName).IncrementRevokedTickets();

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

        private sealed class TicketDetails
        {
            public const string FormatColumns = "\t\tServed\t\t\tRevoked";
            
            private int _servedTickets = 0;
            private int _revokedTickets = 0;

            public void IncrementServedTickets() => _servedTickets++;
            public void IncrementRevokedTickets() => _revokedTickets++;

            public override string ToString() => $"\t\t{_servedTickets}\t\t\t{_revokedTickets}";
        }
    }
}