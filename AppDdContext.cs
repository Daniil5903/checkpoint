using Microsoft.EntityFrameworkCore;
using checkpoint.Models; // Убедитесь, что пространство имен соответствует вашему проекту

namespace checkpoint // Убедитесь, что пространство имен соответствует вашему проекту
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employees { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
    }
}