using System.ComponentModel.DataAnnotations;
namespace checkpoint.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string?  Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов")]
        public string? Surname { get; set; }

        [StringLength(50)]
        public string? Patronymic { get; set; }

    }
}