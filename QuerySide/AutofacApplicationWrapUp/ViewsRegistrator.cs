using Autofac;
using CustomerQueueViews;

namespace AutofacApplicationWrapUp
{
    public sealed class ViewsRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CountersView>().SingleInstance();
        }
    }
}