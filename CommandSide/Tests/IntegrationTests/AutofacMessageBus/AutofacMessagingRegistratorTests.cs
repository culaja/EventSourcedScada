using Autofac;
using AutofacApplicationWrapUp;
using Xunit;

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