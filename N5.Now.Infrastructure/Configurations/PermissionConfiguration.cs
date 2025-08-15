using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.Now.Domain.Entities;

namespace N5.Now.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> b)
    {
        b.ToTable("permission");
        b.HasKey(x => x.Id);

        b.Property(x => x.EmployeeFirstName).HasMaxLength(100).IsRequired();
        b.Property(x => x.EmployeeLastName).HasMaxLength(100).IsRequired();
        b.Property(x => x.PermissionTypeId).IsRequired();
        b.Property(x => x.PermissionDate).IsRequired();

        b.HasOne(x => x.PermissionType)
         .WithMany(x => x.Permissions)
         .HasForeignKey(x => x.PermissionTypeId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
