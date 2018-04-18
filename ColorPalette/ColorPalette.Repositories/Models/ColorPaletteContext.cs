using System.Data.Entity;
using ColorPalette.Repositories.Interfaces;

namespace ColorPalette.Repositories.Models
{
    public class ColorPaletteContext : DbContext, IColorPaletteContext
    {
        public ColorPaletteContext() : base("name=ColorPaletteContext")
        {
            // Set log variable so that we can make sure the SQL queries generated aren't inefficient
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Picture> Pictures { get; set; }
    }
}
