using Autofac;
using CustomerQueueViews;

namespace AutofacApplicationWrapUp.QuerySide
{
    public sealed class ViewsRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewHolder>().SingleInstance();
        }
    }
}