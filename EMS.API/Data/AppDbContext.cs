using EMS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }

        // 1. Registered the new Department table
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            // 2. Automatically seed the 10 professional departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "Developer" },
                new Department { Id = 3, Name = "Project Manager" },
                new Department { Id = 4, Name = "Software Engineer" },
                new Department { Id = 5, Name = "QA / Test Engineer" },
                new Department { Id = 6, Name = "UI/UX Designer" },
                new Department { Id = 7, Name = "DevOps Engineer" },
                new Department { Id = 8, Name = "Data Analyst" },
                new Department { Id = 9, Name = "IT Support & Security" },
                new Department { Id = 10, Name = "Finance & Operations" }
            );
        }
    }
}