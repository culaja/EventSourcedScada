using Autofac;
using CommandSide.CommandSidePorts;
using CommandSide.CommandSidePorts.System;

namespace AutofacApplicationWrapUp.CommandSide
{
    public sealed class StandardPortsRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StandardLocalTimeProvider>().As<ILocalTimeProvider>();
        }
    }
}