using System.ComponentModel.DataAnnotations;

namespace checkpoint.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Должность обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Длина должности не должна превышать 50 символов")]
        public string?  Position { get; set; }

        // Передаваемое свойство навигации (отношение "один ко многим")
        public ICollection<Pass>? Passes { get; set; }
    }
}