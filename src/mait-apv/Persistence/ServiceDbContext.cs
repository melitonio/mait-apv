using Dto;
using Entities;
using MAIT.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options) : EmptyDbContext(options)
{
    public DbSet<Apv> Aparatados { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
        builder.Properties<IEnumerable<LocalizacionDto>>().HaveConversion<Converter<IEnumerable<LocalizacionDto>>>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var comparer = new ValueComparer<IEnumerable<LocalizacionDto>>(
            static (c1, c2) => c1 != null && c1.SequenceEqual(c2 ?? Enumerable.Empty<LocalizacionDto>()),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );

        modelBuilder.Entity<Apv>()
            .Property(a => a.Localiaciones)
            .HasConversion<Converter<IEnumerable<LocalizacionDto>>>()
            .Metadata.SetValueComparer(comparer);
    }
}
