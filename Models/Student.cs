using System;
using System.ComponentModel.DataAnnotations;

namespace checkpoint.Models
{
    public class Student
    {
        [Key] // Указывает, что это первичный ключ
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов")]
        public string? Surname { get; set; }

        [StringLength(50)]
        public string? Patronymic { get; set; } // Отчество может быть необязательным

        [DataType(DataType.Date)] // Указывает, что это тип данных "дата"
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Формат даты
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Группа обязательна для заполнения")]
        [StringLength(10, ErrorMessage = "Длина группы не должна превышать 10 символов")]
        public string? Group { get; set; }

        [Required(ErrorMessage = "Специальность обязательна для заполнения")]
        [StringLength(100, ErrorMessage = "Длина специальности не должна превышать 100 символов")]
        public string? Specialty { get; set; }


    }
}