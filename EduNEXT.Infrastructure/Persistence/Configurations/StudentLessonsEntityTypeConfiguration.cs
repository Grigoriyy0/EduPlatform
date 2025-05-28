using EduNEXT.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class StudentLessonsEntityTypeConfiguration : IEntityTypeConfiguration<StudentLessons>
{
    public void Configure(EntityTypeBuilder<StudentLessons> builder)
    {
        builder.HasKey(sl => new { sl.StudentId, sl.LessonId });
        
        builder.HasOne(sl => sl.Student)
            .WithMany(s => s.StudentLessons)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(sl => sl.Lesson)
            .WithMany(s => s.StudentLessons)
            .OnDelete(DeleteBehavior.SetNull);
    }
}