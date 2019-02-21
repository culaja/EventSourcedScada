using Common;
using Common.Messaging;
using Ports.Messaging;
using Shared.CustomerQueue;

namespace DomainServices.EventHandlers
{
    public sealed class CustomerQueueEventRemoteNotifier : EventHandler<CustomerQueueEvent>
    {
        private readonly IRemoteMessageBus _remoteMessageBus;

        public CustomerQueueEventRemoteNotifier(IRemoteMessageBus remoteMessageBus)
        {
            _remoteMessageBus = remoteMessageBus;
        }

        public override Result Handle(CustomerQueueEvent e) => _remoteMessageBus
            .Send(e)
            .ToOkResult();
    }
}