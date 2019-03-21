using System;
using System.Collections.Generic;
using System.Linq;
using QuerySide.QueryCommon;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer.Events;

namespace QuerySide.Views.QueueHistory
{
    public sealed class QueueHistoryView : SynchronizedView,
        IHandle<TicketIssued>,
        IHandle<OutOfLineTicketIssued>,
        IHandle<CustomerAssignedToCounter>,
        IHandle<OutOfLineCustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>
    {
        private int _waitingCustomerCount = 0;
        private readonly Dictionary<Guid, Ticket> _ticketById = new Dictionary<Guid, Ticket>();

        public IReadOnlyList<Ticket> TicketHistory => _ticketById.Values.OrderBy(t => t.DrawTime).ToList();
        
        public void Handle(TicketIssued e) => _ticketById.Add(e.TicketId, new Ticket(e.TicketId, e.TicketNumber, e.Timestamp, _waitingCustomerCount++));
        public void Handle(OutOfLineTicketIssued e) => _ticketById.Add(e.TicketId, new Ticket(e.TicketId, e.TicketNumber, e.Timestamp, _waitingCustomerCount));

        public void Handle(CustomerAssignedToCounter e)
        {
            _waitingCustomerCount--;   
            _ticketById[e.TicketId].Handle(e);
        }

        public void Handle(OutOfLineCustomerAssignedToCounter e) => _ticketById[e.TicketId].Handle(e);
        public void Handle(CustomerServedByCounter e) => _ticketById[e.TicketId].Handle(e);
    }
}