using Autofac;
using QuerySidePorts;
using WebsocketClientNotifier;

namespace AutofacApplicationWrapUp.QuerySide
{
    public sealed class ClientNotificationRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebSocketNotifier>().As<IClientNotifier>().SingleInstance();
        }
    }
}