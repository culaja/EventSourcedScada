using Autofac;
using EventStore;
using Ports;
using Shared.CustomerQueue;

namespace AutofacApplicationWrapUp
{
    public sealed class EventStoreRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new EventStoreSubscriptionProvider(
                "mongodb://localhost:27017/", 
                "CustomerQueue",
                "localhost"))
                .As<IEventStoreSubscriptionProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IEventStoreSubscriptionProvider>().MakeSubscriptionFor<CustomerQueueSubscription>())
                .As<IEventStoreSubscription<CustomerQueueSubscription>>()
                .SingleInstance();
        }
    }
}