using System;
using System.Collections.Generic;
using QuerySide.QueryCommon;
using Shared.CustomerQueue;
using Shared.TicketIssuer;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class AssignedCustomerView : SynchronizedView, IAssignedCustomerViewHandlers
    {
        private readonly Dictionary<Guid, int> _ticketIdToNumber = new Dictionary<Guid, int>();
        private int _numberOfTicketsInQueue = 0;
        private int _assignedTicketNumber = 0;

        public int TicketNumber => _assignedTicketNumber;
        public int WaitingCustomerCount => _numberOfTicketsInQueue;
        public int ExpectedWaitingTimeInSeconds => -1;

        public void Handle(TicketIssued e) => _ticketIdToNumber.Add(e.TicketId, e.TicketNumber);

        public void Handle(CustomerEnqueued e) => _numberOfTicketsInQueue++;

        public void Handle(CustomerAssignedToCounter e)
        {
            _numberOfTicketsInQueue--;
            _assignedTicketNumber = _ticketIdToNumber[e.TicketId];
        }

        public void Handle(CustomerServedByCounter e)
        {
            _assignedTicketNumber = 0;
            _ticketIdToNumber.Remove(e.TicketId);
        }
    }
}