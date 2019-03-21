using Autofac;
using CommandSide.DomainServices;

namespace AutofacApplicationWrapUp.CommandSide
{
    public class CommandSideServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CommandSideInitializer>();
        }
    }
}