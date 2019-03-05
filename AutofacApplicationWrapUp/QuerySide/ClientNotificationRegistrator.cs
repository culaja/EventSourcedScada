using Autofac;
using QuerySide.Adapters.WebsocketClientNotifier;
using QuerySide.QuerySidePorts;

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