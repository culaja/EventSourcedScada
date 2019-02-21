using System;
using System.Collections.Generic;
using System.Text;
using QueryCommon;
using Shared.CustomerQueue;

namespace CustomerQueueViews
{
    public sealed class TicketQueueView : View,
        IHandle<TicketAdded>,
        IHandle<CustomerTaken>,
        IHandle<CustomerServed>,
        IHandle<CustomerRevoked>
    {
        private readonly Dictionary<Guid, TicketState> _dictionary = new Dictionary<Guid, TicketState>();

        private readonly List<TicketState> _ticketStates = new List<TicketState>();
        public IReadOnlyList<TicketState> TicketStates => _ticketStates;

        public void Handle(TicketAdded e) => TicketStateFrom(e.TicketId).ToWaiting(e.TicketNumber.ToString());

        public void Handle(CustomerTaken e) => TicketStateFrom(e.TicketId).ToServing();

        public void Handle(CustomerServed e) => TicketStateFrom(e.TicketId).ToServed();

        public void Handle(CustomerRevoked e) => TicketStateFrom(e.TicketId).ToRevoked();
        
        private TicketState TicketStateFrom(Guid ticketId)
        {
            if (!_dictionary.TryGetValue(ticketId, out var ticketDetails))
            {
                ticketDetails = new TicketState();
                _dictionary[ticketId] = ticketDetails;
                _ticketStates.Add(ticketDetails);
            }

            return ticketDetails;
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("***************************Ticket queue ***********************************");
            foreach (var t in _ticketStates) builder.Append($"{t.Number}\t"); builder.AppendLine();
            foreach (var t in _ticketStates) builder.Append($"{t.State}\t"); builder.AppendLine();
            builder.AppendLine("***************************************************************************");
            builder.AppendLine();
            return builder.ToString();
        }
        
        public sealed class TicketState
        {
            public const string FormatColumns = "\t\tServed\t\t\tRevoked";

            public string Number { get; private set; } = "";
            public char State { get; private set; }

            public void ToWaiting(string name)
            {
                Number = name;
                State = 'W';
            }

            public void ToServing() => State = 'C';
            public void ToServed() => State = 'S';
            public void ToRevoked() => State = 'R';
        }
    }
}