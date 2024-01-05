using Microsoft.EntityFrameworkCore;
using StudentHub.Models;

namespace StudentHub.Data;

public class StudentDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }
}