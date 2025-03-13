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
            //Database.Migrate(); // Автоматически применяет миграции и создаёт базу, если её нет
        }

        public DbSet<Employer> Employers { get; set; } //таблица Employer, содержащая данные о сотрудниках.
        public DbSet<Student> Students { get; set; } //таблица Students, содержащая данные о студентах.
        public DbSet<Pass> Passes{ get; set; } //таблица Pass, хранящая информацию о пропусках.
        public DbSet<CheckpointEmployer> CheckpointEmployers { get; set; } //таблица CheckpointEmployer, хранящая информацию о сотрудниках КПП.
        public DbSet<Visitor> Visitors { get; set; } //таблица Visitor, хранящая информацию о посетителях.
    }
}
