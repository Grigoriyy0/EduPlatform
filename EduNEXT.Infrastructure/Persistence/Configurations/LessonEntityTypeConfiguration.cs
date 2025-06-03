using EduNEXT.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduNEXT.Infrastructure.Persistence.Configurations;

public class LessonEntityTypeConfiguration : IEntityTypeConfiguration<StudentTimeSlots>
{
    public void Configure(EntityTypeBuilder<StudentTimeSlots> builder)
    {
        builder.HasKey(l => l.Id);
    }
}