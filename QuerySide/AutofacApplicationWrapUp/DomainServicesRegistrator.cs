using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Services;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public sealed class DomainServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            RegisterAllMessageHandlersForAllDomainMessages(containerBuilder);
            RegisterOtherDomainServices(containerBuilder);
        }

        private void RegisterAllMessageHandlersForAllDomainMessages(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AutofacMessagingRegistrator(
                new List<Assembly>
                {
                },
                new List<Assembly>()
                {
                }));
        }

        private void RegisterOtherDomainServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<QuerySideInitializer>();
        }
    }
}