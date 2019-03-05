using Autofac;
using CommandSidePorts.Repositories;
using InMemory;

namespace AutofacApplicationWrapUp.CommandSide
{
    public sealed class RepositoryRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerQueueInMemoryRepository>().As<ICustomerQueueRepository>().SingleInstance();
        }
    }
}