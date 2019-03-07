using Common;
using Common.Messaging;
using QuerySide.QuerySidePorts;
using QuerySide.Views.CustomerQueueViews;

namespace QuerySide.Services.ClientEventHandlers
{
    public sealed class NewClientConnectedHandler : MessageHandler<NewClientConnected>
    {
        private readonly CustomerQueueViewHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public NewClientConnectedHandler(
            CustomerQueueViewHolder viewHolder,
            IClientNotifier clientNotifier)
        {
            _viewHolder = viewHolder;
            _clientNotifier = clientNotifier;
        }
        
        public override Result Handle(NewClientConnected message) => _viewHolder
            .ForEachView(_clientNotifier.NotifyAll).ToOkResult();
    }
}