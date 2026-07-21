using DataProcessorService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataProcessorService.Infrastructure.Context;

internal sealed class SensorsDataContext : DbContext
{
    public SensorsDataContext(DbContextOptions<SensorsDataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<SensorDataEntity> Sensors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SensorDataEntity>(x =>
        {
            x.HasKey(x => x.Id);

            x.HasIndex(x => x.DataType)
                .IsUnique(false);

            x.HasIndex(x => x.PlacementName)
                .IsUnique(false);

            x.HasIndex(x => x.DataType)
                .IsUnique(false);
        });
    }
}
