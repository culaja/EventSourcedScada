using Common;
using Common.Messaging;
using CustomerQueueViews;
using Ports;

namespace Services.ClientEventHandlers
{
    public sealed class NewClientConnectedHandler : MessageHandler<NewClientConnected>
    {
        private readonly ViewHolder _viewHolder;
        private readonly IClientNotifier _clientNotifier;

        public NewClientConnectedHandler(
            ViewHolder viewHolder,
            IClientNotifier clientNotifier)
        {
            _viewHolder = viewHolder;
            _clientNotifier = clientNotifier;
        }
        
        public override Result Handle(NewClientConnected message) => _viewHolder
            .ForEachView(_clientNotifier.NotifyAll).ToOkResult();
    }
}