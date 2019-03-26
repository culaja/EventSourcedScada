using Common;
using Common.Messaging;
using QuerySide.QuerySidePorts;
using QuerySide.Views;
using Shared.CustomerQueue;

namespace QuerySide.Services.EventHandlers
{
    public sealed class ViewRefreshFromCustomerQueueEventHandler : EventHandler<CustomerQueueEvent>
    {
        private readonly ViewsHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public ViewRefreshFromCustomerQueueEventHandler(
            ViewsHolder viewHolder,
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