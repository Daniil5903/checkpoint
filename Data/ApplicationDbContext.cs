using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using checkpoint.Models;

namespace checkpoint.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Конструктор, который принимает параметры конфигурации базы данных.
        // — Передаёт эти параметры в базовый класс DbContext с помощью : base(options).
        // — Это позволяет настраивать подключение к базе через Dependency Injection.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //  Удалили Database.Migrate()
        }
        public DbSet<Employee> Employees { get; set; } = null!;  //таблица Employee, содержащая данные о сотрудниках.
        public DbSet<Student> Students { get; set; } = null!; //таблица Students, содержащая данные о студентах.
        public DbSet<Pass> Passes{ get; set; } = null!; //таблица Pass, хранящая информацию о пропусках.
        public DbSet<CheckpointEmployee> CheckpointEmployees { get; set; } = null!; //таблица CheckpointEmployer, хранящая информацию о сотрудниках КПП.
        public DbSet<Visitor> Visitors { get; set; } = null!; //таблица Visitor, хранящая информацию о посетителях.
    }
}
