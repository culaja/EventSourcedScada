using Autofac;
using Ports;
using WebsocketClientNotifier;

namespace AutofacApplicationWrapUp
{
    public sealed class ClientNotificationRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebSocketNotifier>().As<IClientNotifier>().SingleInstance();
        }
    }
}