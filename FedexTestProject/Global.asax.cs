using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using AutoMapper;
using FedexTestProject.Core;

namespace FedexTestProject.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider => _containerProvider;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            InitCoreDependencies(builder);
            InitAutomapper(builder);
            // ... continue registering dependencies...

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());
        }

        private void InitCoreDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<TrackingManager>().
                AsSelf().
                InstancePerRequest().
                PropertiesAutowired();
            builder.RegisterType<TextFileProcessor>().
                AsSelf().
                InstancePerRequest().
                PropertiesAutowired();
            builder.RegisterType<XlsFileProcessor>().
                AsSelf().
                InstancePerRequest().
                PropertiesAutowired();
        }

        private void InitAutomapper(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg => {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }
}