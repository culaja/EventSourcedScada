using QuerySide.QueryCommon;
using Shared.CustomerQueue;
using Shared.TicketIssuer;

namespace QuerySide.Views.AssigningCustomer
{
    public interface IAssignedCustomerViewHandlers :
        IHandle<TicketIssued>,
        IHandle<CustomerEnqueued>,
        IHandle<CustomerAssignedToCounter>,
        IHandle<CustomerServedByCounter>
    {
    }
}