using Autofac;
using QuerySide.Services;

namespace AutofacApplicationWrapUp.QuerySide
{
    public sealed class QuerySideServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<QuerySideInitializer>();
        }
    }
}