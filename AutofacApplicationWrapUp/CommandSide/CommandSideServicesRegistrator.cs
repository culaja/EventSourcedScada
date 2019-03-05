using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Domain;
using DomainServices;
using DomainServices.EventHandlers;
using Shared.CustomerQueue;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp.CommandSide
{
    public class CommandSideServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AggregateConstructor>();
        }
    }
}