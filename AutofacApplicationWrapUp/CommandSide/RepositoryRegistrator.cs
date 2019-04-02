using Autofac;
using CommandSide.Adapters.InMemory;
using CommandSide.CommandSidePorts.Repositories;

namespace AutofacApplicationWrapUp.CommandSide
{
    public sealed class RepositoryRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RemoteRepository>().As<IRemoteRepository>().SingleInstance();
        }
    }
}