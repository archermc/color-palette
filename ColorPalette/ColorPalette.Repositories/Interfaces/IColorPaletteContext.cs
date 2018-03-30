using System;
using ColorPalette.Repositories.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IColorPaletteContext : IDisposable
    {
        DbSet<Picture> Pictures { get; }

        Task<int> SaveChangesAsync();
    }
}
