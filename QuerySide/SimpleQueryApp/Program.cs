using System;
using Autofac;
using AutofacApplicationWrapUp;
using Services;

namespace SimpleQueryApp
{
    public class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MainRegistrator>();
            var container = builder.Build();

            var initializer = container.Resolve<QuerySideInitializer>();
            initializer.Initialize();

            Console.ReadLine();
        }
    }
}