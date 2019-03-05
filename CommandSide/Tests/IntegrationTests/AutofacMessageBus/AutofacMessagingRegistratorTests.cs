using Autofac;
using AutofacApplicationWrapUp;

namespace CommandSide.Tests.IntegrationTests.AutofacMessageBus
{
    public sealed class AutofacMessagingRegistratorTests
    {
        private readonly IContainer _container;

        public AutofacMessagingRegistratorTests()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MainRegistrator>();
            _container = containerBuilder.Build();
        }
    }
}