using System.Threading.Tasks;
using ColorPalette.Repositories.Interfaces;
using ColorPalette.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ColorPalette.Repositories.Implementations
{
    public class ColorPaletteContext : DbContext, IColorPaletteContext
    {
        public ColorPaletteContext() : base(new DbContextOptions<ColorPaletteContext>())
        {
            // Set log variable so that we can make sure the SQL queries generated aren't inefficient
            // Database.Log = (string s) => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Picture> Pictures { get; set; }
        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

    }
}
