namespace Truckoom.Maintenance.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtension
{
    public static void MappingDefaultValues(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Models.NLog>()
            .Property(u => u.Id)
           .HasDefaultValueSql("NEWID()");
    }
}
