using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using hw_service_try2.Bl;
using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Dal;
using hw_service_try2.Dal.Interfaces;

namespace hw_service_try2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // enable logging
            NLog.LogManager.EnableLogging();
            
            // json formatter conf
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Autofac DI
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DbCardRepository>().As<ICardRepository>();
            builder.RegisterType<CardBusinessLayer>().As<ICardBusinessLayer>();
            builder.RegisterType<DbGroupRepository>().As<IGroupRepository>();
            builder.RegisterType<GroupBusinessLayer>().As<IGroupBusinessLayer>();
            builder.RegisterType<DbCardTranslator>().As<ICardTranslator>();
            builder.RegisterType<CardTester>().As<ICardTester>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "TranslateApi",
                routeTemplate: "api/translate/{action}/{word}",
                defaults: new { controller = "translate" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
