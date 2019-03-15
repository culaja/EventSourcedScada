using QuerySide.QueryCommon;
using Shared.CustomerQueue;
using Shared.TicketIssuer;

namespace QuerySide.Views.AssigningCustomer
{
    public sealed class AssignedCustomerViewGroup : ViewGroup<AssignedCustomerView>, IAssignedCustomerViewHandlers
    {
        public void Handle(TicketIssued e) => PassEventToAllViews(e);

        public void Handle(CustomerEnqueued e) => PassEventToAllViews(e);

        public void Handle(CustomerAssignedToCounter e) => PassEventToViewWithId(e.CounterId.ToCounterNumber(), e);

        public void Handle(CustomerServedByCounter e) => PassEventToViewWithId(e.CounterId.ToCounterNumber(), e);
    }
}