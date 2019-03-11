using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacApplicationWrapUp;
using CommandSide.DomainServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuerySide.Services;
using WebApp.Middlewares;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddMvc().AddControllersAsServices();
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new MainRegistrator());
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            
            InitializeServiceLayer(container);
            
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMiddleware<ExceptionToHttpResponseMiddleware>();

            app.UseStaticFiles();
            app.UseMvc();
        }

        private static void InitializeServiceLayer(IComponentContext componentContext)
        {
            componentContext.Resolve<CommandSideInitializer>().Initialize();
            componentContext.Resolve<QuerySideInitializer>().Initialize();
        }
    }
}