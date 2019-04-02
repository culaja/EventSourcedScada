using System.Collections.Generic;
using System.Reflection;
using Autofac;
using CommandSide.Domain.RemoteDomain;
using CommandSide.DomainServices.RemoteHandlers.EventHandlers;
using CommonAdapters.AutofacMessageBus;
using QuerySide.Services;
using QuerySide.Services.EventHandlers;
using Shared.Remote;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public sealed class AutofacMessageBusRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacMessagingRegistrator(
                new List<Assembly>
                {
                    RemoteSharedEventAssembly,
                    CommandSideEventAssembly,
                    QuerySideEventAssembly
                },
                new List<Assembly>
                {
                    CommandSideMessageHandlerAssembly,
                    QuerySideMessageHandlerAssembly
                }));
        }

        private static Assembly RemoteSharedEventAssembly => typeof(RemoteEvent).Assembly;

        private static Assembly CommandSideEventAssembly => typeof(Remote).Assembly;

        private static Assembly QuerySideEventAssembly => typeof(NewClientConnected).Assembly;

        private static Assembly CommandSideMessageHandlerAssembly => typeof(RemotePersistenceHandler).Assembly;

        private static Assembly QuerySideMessageHandlerAssembly => typeof(ViewRefreshFromRemoteEventHandler).Assembly;
    }
}