using checkpoint.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore;

namespace checkpoint.Data
{
    public class ApplicationDbContext : IdentityDbContext<AuthUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Pass> Passes { get; set; } = null!;
        public DbSet<CheckpointEmployee> CheckpointEmployees { get; set; } = null!;
        public DbSet<Visitor> Visitors { get; set; } = null!;
    }
}
