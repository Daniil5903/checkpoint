using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace checkpoint.Models
{
    public class Pass
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Номер пропуска обязателен для заполнения")]
        [StringLength(20, ErrorMessage = "Длина номера пропуска не должна превышать 20 символов")]
        public string? PassNumber { get; set; }

        [Required(ErrorMessage = "Тип пропуска обязателен для заполнения")]
        [StringLength(20, ErrorMessage = "Длина типа пропуска не должна превышать 20 символов")]
        public string? PassType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpirationDate { get; set; } // Может быть null

        [Required(ErrorMessage = "Цель посещения обязательна для заполнения")]
        [StringLength(200, ErrorMessage = "Длина цели посещения не должна превышать 200 символов")]
        public string? Purpose { get; set; }

        public bool IsActive { get; set; }

        // Foreign Keys и Navigation Properties

        // Student
        public int? StudentId { get; set; } // Сделал nullable, чтобы можно было создать пропуск без привязки к студенту
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        // Employer
        public int? EmployeeId { get; set; } // Сделал nullable
        [ForeignKey("EmployeeId")]
        public Employer? Employee { get; set; }

        // Visitor
        public int? VisitorId { get; set; } // Сделал nullable
        [ForeignKey("VisitorId")]
        public Visitor? Visitor { get; set; }
    }
}