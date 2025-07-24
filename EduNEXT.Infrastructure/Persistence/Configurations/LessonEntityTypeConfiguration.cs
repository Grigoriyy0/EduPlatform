using EduNEXT.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.StartTime)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(x => x.StudentId)
            .IsRequired();
        
        builder.HasOne(x => x.Student)
            .WithMany(s => s.Lessons)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}