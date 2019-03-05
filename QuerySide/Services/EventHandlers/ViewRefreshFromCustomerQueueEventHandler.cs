using Common;
using QuerySide.QuerySidePorts;
using QuerySide.Views.CustomerQueueViews;
using Shared.CustomerQueue;

namespace QuerySide.Services.EventHandlers
{
    public sealed class ViewRefreshFromCustomerQueueEventHandler : Common.Messaging.EventHandler<CustomerQueueEvent>
    {
        private readonly ViewHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public ViewRefreshFromCustomerQueueEventHandler(
            ViewHolder viewHolder,
            IClientNotifier clientNotifier)
        {
            _viewHolder = viewHolder;
            _clientNotifier = clientNotifier;
        }
        
        public override Result Handle(CustomerQueueEvent e)
        {
            _viewHolder.Apply(e);
            _viewHolder.ForEachView(_clientNotifier.NotifyAll);
            return Result.Ok();
        }
    }
}