using Autofac;
using Module = Autofac.Module;

namespace AutofacApplicationWrapup
{
    public sealed class MainRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<DomainServicesRegistrator>();
            builder.RegisterModule<EventStoreRegistrator>();
            builder.RegisterModule<RemoteMessagingRegistrator>();
            builder.RegisterModule<ViewsRegistrator>();
        }
    }
}