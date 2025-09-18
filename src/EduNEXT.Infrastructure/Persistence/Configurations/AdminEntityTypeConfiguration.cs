using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.Email, opt =>
        {
            opt.Property(x => x.Value)
                .HasColumnName("Email")
                .IsRequired();

            opt.WithOwner();
        });
        
        builder.Property(x => x.PasswordHash).IsRequired();
    }
}