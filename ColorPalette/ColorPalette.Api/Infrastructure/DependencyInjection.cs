using ColorPalette.Repositories.Implementations;
using ColorPalette.Repositories.Interfaces;
using ColorPalette.Services.Implementations;
using ColorPalette.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ColorPalette.Api.Infrastructure
{
    internal static class DependencyInjection
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddDbContext<ColorPaletteContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ColorPalette;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddTransient<IPicturesRepository, PicturesRepository>();
            services.AddTransient<IPicturesService, PicturesService>();
        }
    }
}
