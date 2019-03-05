using System.Collections.Generic;
using System.Reflection;
using Autofac;
using CommandSide.DomainServices;
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