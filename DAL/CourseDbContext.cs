using CourseApi.Configuration;
using CourseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.DAL
{
    public class CourseDbContext:DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}
