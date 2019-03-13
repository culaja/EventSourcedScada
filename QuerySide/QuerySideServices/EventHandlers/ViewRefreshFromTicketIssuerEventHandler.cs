using Common;
using Common.Messaging;
using QuerySide.QuerySidePorts;
using QuerySide.Views.CustomerQueueViews;
using Shared.TicketIssuer;

namespace QuerySide.Services.EventHandlers
{
    public sealed class ViewRefreshFromTicketIssuerEventHandler : EventHandler<TicketIssuerEvent>
    {
        private readonly CustomerQueueViewHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public ViewRefreshFromTicketIssuerEventHandler(
            CustomerQueueViewHolder viewHolder,
            IClientNotifier clientNotifier)
        {
            _viewHolder = viewHolder;
            _clientNotifier = clientNotifier;
        }
        
        public override Result Handle(TicketIssuerEvent e)
        {
            _viewHolder.Apply(e);
            _viewHolder.ForEachView(_clientNotifier.NotifyAll);
            return Result.Ok();
        }
    }
}