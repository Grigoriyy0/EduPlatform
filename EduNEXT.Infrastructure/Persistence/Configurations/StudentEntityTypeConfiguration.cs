using EduNEXT.Core.Domain.Entities;
using EduNEXT.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Firstname)
            .IsRequired();
        
        builder.Property(x => x.Lastname)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasConversion(x => x.Value,
                x => EmailAddress.Create(x).Value)
            .IsRequired();
        
        builder.HasMany(s => s.Lessons)
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}