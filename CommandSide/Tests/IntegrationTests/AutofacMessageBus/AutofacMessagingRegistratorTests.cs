using Autofac;
using AutofacApplicationWrapup;

namespace Tests.IntegrationTests.AutofacMessageBus
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