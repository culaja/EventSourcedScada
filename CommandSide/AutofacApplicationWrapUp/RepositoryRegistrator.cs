using Autofac;
using InMemory;
using Ports.Repositories;

namespace AutofacApplicationWrapup
{
    public sealed class RepositoryRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerQueueInMemoryRepository>().As<ICustomerQueueRepository>().SingleInstance();
        }
    }
}