using Autofac;
using QuerySide.Views;

namespace AutofacApplicationWrapUp.QuerySide
{
    public sealed class ViewsRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewsHolder>().SingleInstance();
        }
    }
}