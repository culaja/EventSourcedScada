using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Services;
using Services.EventHandlers;
using Shared.CustomerQueue;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp.QuerySide
{
    public sealed class QuerySideServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<QuerySideInitializer>();
        }
    }
}