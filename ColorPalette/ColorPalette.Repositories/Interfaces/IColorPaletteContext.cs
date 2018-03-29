using ColorPalette.Repositories.Models;
using System.Data.Entity;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IColorPaletteContext
    {
        DbSet<Picture> Pictures { get; }
    }
}
