using System;
using System.Collections.Generic;
using Common;
using QuerySide.QueryCommon;
using Shared.CustomerQueue;
using Shared.CustomerQueue.Events;
using Shared.TicketIssuer;
using Shared.TicketIssuer.Events;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class AssignedCustomerGroupView : GroupView<AssignedCustomer>,
        IHandle<TicketIssued>,
        IHandle<OutOfLineTicketIssued>,
        IHandle<CustomerEnqueued>,
        IHandle<OutOfLineCustomerAssignedToCounter>,
        IHandle<CustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>,
        IHandle<WaitingCustomersRemoved>
    {
        private int _ticketsInQueue = 0;
        private readonly Dictionary<Guid, int> _ticketIdToNumber = new Dictionary<Guid, int>();
        private readonly Dictionary<Id, int> _counterToTicketNumber = new Dictionary<Id, int>();
        
        public override AssignedCustomer GenerateViewFor(Id id) => new AssignedCustomer(
            _counterToTicketNumber.MaybeGetValue(id).Unwrap(),
            _ticketsInQueue,
            0);

        public void Handle(OutOfLineTicketIssued e) => _ticketIdToNumber.Add(e.TicketId, e.TicketNumber);

        public void Handle(TicketIssued e) => _ticketIdToNumber.Add(e.TicketId, e.TicketNumber);

        public void Handle(CustomerEnqueued e) => _ticketsInQueue++;

        public void Handle(CustomerAssignedToCounter e)
        {
            _ticketsInQueue--;
            _counterToTicketNumber[e.CounterId.ToCounterId()] = _ticketIdToNumber[e.TicketId];
        }

        public void Handle(OutOfLineCustomerAssignedToCounter e)
        {
            _counterToTicketNumber[e.CounterId.ToCounterId()] = _ticketIdToNumber[e.TicketId];
        }

        public void Handle(CustomerServedByCounter e)
        {
            _counterToTicketNumber[e.CounterId.ToCounterId()] = 0;
            _ticketIdToNumber.Remove(e.TicketId);
        }

        public void Handle(WaitingCustomersRemoved e)
        {
            _ticketsInQueue -= e.TicketIds.Count;
            e.TicketIds.Map(t => _ticketIdToNumber.Remove(t));
        }
    }
}