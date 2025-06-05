using Entities;
using MAIT.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options) : EmptyDbContext(options)
{
    public DbSet<Apv> Aparatados { get; set; }
    public DbSet<Localizacion> Localizaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Localizacion>().HasIndex(l => new { l.ApvId, l.Nombre }).IsUnique();
    }
}
