using Common;
using Common.Messaging;
using QuerySide.QuerySidePorts;
using QuerySide.Views;
using Shared.Remote;

namespace QuerySide.Services.EventHandlers
{
    public sealed class ViewRefreshFromRemoteEventHandler : EventHandler<RemoteEvent>
    {
        private readonly ViewsHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public ViewRefreshFromRemoteEventHandler(
            ViewsHolder viewHolder,
            IClientNotifier clientNotifier)
        {
            _viewHolder = viewHolder;
            _clientNotifier = clientNotifier;
        }

        public override Result Handle(RemoteEvent e)
        {
            _viewHolder.Apply(e);
            _viewHolder.ForEachView(_clientNotifier.NotifyAll);
            return Result.Ok();
        }
    }
}