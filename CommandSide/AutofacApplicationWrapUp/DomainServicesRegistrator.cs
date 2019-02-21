using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Domain;
using DomainServices;
using DomainServices.EventHandlers;
using Shared.CustomerQueue;
using Module = Autofac.Module;

namespace AutofacApplicationWrapup
{
    public class DomainServicesRegistrator : Module
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
                    typeof(CustomerQueue).Assembly,
                    typeof(CustomerQueueEvent).Assembly
                },
                new List<Assembly>()
                {   
                    typeof(CustomerQueueEventRemoteNotifier).Assembly
                }));
        }

        private void RegisterOtherDomainServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AggregateConstructor>();
        }
    }
}