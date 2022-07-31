using Autofac;
using Autofac.Integration.WebApi;
using Helpers;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using ViewModels;

namespace EgyptianRecipes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.EnableCors(new EnableCorsAttribute("*", headers: "*", methods: "*"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .PropertiesAutowired().InstancePerRequest();
            builder.RegisterType(typeof(EntitiesContext)).As(typeof(EntitiesContext)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Generic<>)).As(typeof(Generic<>)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(UnitOfWork)).InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CustomerService).Assembly)
                .AsSelf()
                .Where(i => i.Name.EndsWith("Service")).InstancePerRequest();

            builder.RegisterType(typeof(SecurityHelper)).InstancePerRequest();
            builder.RegisterGeneric(typeof(ResultViewModel<>)).InstancePerRequest();

            IContainer ioc = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(ioc);
        }
    }
}
