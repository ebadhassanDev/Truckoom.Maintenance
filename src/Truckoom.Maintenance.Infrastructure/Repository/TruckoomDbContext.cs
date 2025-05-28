namespace Truckoom.Maintenance.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Truckoom.Maintenance.Core.Models;

/// <summary>
/// <inheritdoc/>
/// </summary>
/// <param name="option"></param>
public class TruckoomDbContext(DbContextOptions<TruckoomDbContext> option) : DbContext(option)
{
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceTasks> ServiceTasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<NLog> NLog { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.Entity<Service>()
           .HasMany(s => s.ServiceTasks)
           .WithOne()
           .OnDelete(DeleteBehavior.Cascade);
    }
}
