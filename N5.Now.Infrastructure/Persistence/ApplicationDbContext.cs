using Microsoft.EntityFrameworkCore;
using N5.Now.Domain.Entities;
using N5.Now.Infrastructure.Configurations;

namespace N5.Now.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<PermissionType> PermissionTypes => Set<PermissionType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionTypeConfiguration());

        modelBuilder.Entity<PermissionType>().HasData(
            new PermissionType { Id = 1, Description = "Read" },
            new PermissionType { Id = 2, Description = "Write" }
        );
    }
}
