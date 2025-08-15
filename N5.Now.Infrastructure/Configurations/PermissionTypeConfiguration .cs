using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.Now.Domain.Entities;

namespace N5.Now.Infrastructure.Configurations;

public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
{
    public void Configure(EntityTypeBuilder<PermissionType> b)
    {
        b.ToTable("permission_type");
        b.HasKey(x => x.Id);
        b.Property(x => x.Description).HasMaxLength(100).IsRequired();
    }
}
