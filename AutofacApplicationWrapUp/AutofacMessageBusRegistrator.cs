using System.Collections.Generic;
using System.Reflection;
using Autofac;
using CommandSide.Domain;
using CommandSide.DomainServices.EventHandlers;
using CommonAdapters.AutofacMessageBus;
using QuerySide.Services;
using QuerySide.Services.EventHandlers;
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
                    CustomerQueueSharedEventAssembly,
                    QuerySideEventAssembly
                },
                new List<Assembly>()
                {   
                    CommandSideMessageHandlerAssembly,
                    QuerySideMessageHandlerAssembly
                }));
        }
        
        private static Assembly CustomerQueueSharedEventAssembly => typeof(CustomerQueue).Assembly;
        
        private static Assembly QuerySideEventAssembly => typeof(NewClientConnected).Assembly;

        private static Assembly CommandSideMessageHandlerAssembly => typeof(CustomerQueuePersistenceHandler).Assembly;
        
        private static Assembly QuerySideMessageHandlerAssembly => typeof(ViewRefreshFromCustomerQueueEventHandler).Assembly;
    }
}