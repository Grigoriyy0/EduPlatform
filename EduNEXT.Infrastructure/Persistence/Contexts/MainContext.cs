using EduNEXT.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduNEXT.Infrastructure.Persistence.Contexts;

public class MainContext : DbContext
{
    public MainContext(DbContextOptions<MainContext> options) : base(options) {}
    
    public DbSet<StudentTimeSlot> LessonsTimeSlots { get; set; }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Lesson> Lessons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly));
    }
}