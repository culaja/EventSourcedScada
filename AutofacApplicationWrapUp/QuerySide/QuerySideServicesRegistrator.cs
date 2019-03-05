using System.Collections.Generic;
using System.Reflection;
using Autofac;
using QuerySide.Services;
using Shared.CustomerQueue;
using Module = Autofac.Module;

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