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
    public DbSet<ServiceTasks> Tasks { get; set; }
    public DbSet<User> User { get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Service>()
            .HasMany(s => s.Tasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
}