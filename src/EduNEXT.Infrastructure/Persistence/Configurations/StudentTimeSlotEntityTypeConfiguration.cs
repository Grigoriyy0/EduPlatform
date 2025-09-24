using EduNEXT.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class StudentTimeSlotEntityTypeConfiguration : IEntityTypeConfiguration<StudentTimeSlot>
{
    public void Configure(EntityTypeBuilder<StudentTimeSlot> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.StartTime)
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .IsRequired();
        
        builder.Property(x => x.Day)
            .IsRequired();

        builder.HasOne(x => x.Student)
            .WithMany(s => s.LessonTimeSlots)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}