using Autofac;
using CommandSide.DomainServices;
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