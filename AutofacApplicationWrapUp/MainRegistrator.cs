using Autofac;
using AutofacApplicationWrapUp.CommandSide;
using AutofacApplicationWrapUp.QuerySide;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public sealed class MainRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterCommonModules(builder);
            RegisterCommandSideModules(builder);
            RegisterQuerySideModules(builder);
        }

        private void RegisterCommonModules(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacMessageBusRegistrator>();
            builder.RegisterModule<EventStoreRegistrator>();
        }

        private void RegisterCommandSideModules(ContainerBuilder builder)
        {
            builder.RegisterModule<StandardPortsRegistrator>();
            builder.RegisterModule<RepositoryRegistrator>();
            builder.RegisterModule<CommandSideServicesRegistrator>();
        }

        private void RegisterQuerySideModules(ContainerBuilder builder)
        {
            builder.RegisterModule<QuerySideServicesRegistrator>();
            builder.RegisterModule<ViewsRegistrator>();
            builder.RegisterModule<ClientNotificationRegistrator>();
        }
    }
}