using Autofac;
using Autofac.Integration.WebApi;
using ColorPalette.Managers;
using ColorPalette.Managers.Interfaces;
using ColorPalette.Repositories;
using ColorPalette.Repositories.Interfaces;
using ColorPalette.Repositories.Models;
using System.Reflection;
using System.Web.Http;

namespace ColorPalette
{
    public static class DependencyConfig
    {
        public static IContainer RegisterDependencies()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ColorPaletteContext>().As<IColorPaletteContext>();
            builder.RegisterType<PicturesRepository>().As<IPicturesRepository>();
            builder.RegisterType<PicturesManager>().As<IPicturesManager>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}