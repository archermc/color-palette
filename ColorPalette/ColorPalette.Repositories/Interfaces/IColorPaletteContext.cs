using ColorPalette.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IColorPaletteContext : IDisposable
    {
        DbSet<Picture> Pictures { get; }

        Task<int> SaveChangesAsync();
    }
}
