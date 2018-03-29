using ColorPalette.Repositories.Interfaces;
using System.Data.Entity;

namespace ColorPalette.Repositories.Models
{
    public class ColorPaletteContext : DbContext, IColorPaletteContext
    {
        public ColorPaletteContext() : base("name=ColorPaletteContext")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Picture> Pictures { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
