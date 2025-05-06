using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace checkpoint.Models
{

    public class Pass
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string? PassNumber { get; set; }
        [Required, StringLength(20)]
        public string? PassType { get; set; }
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Required, StringLength(200)]
        public string? Purpose { get; set; }
        public bool IsActive { get; set; }
        // Новый универсальный внешний ключ
        public int OwnerId { get; set; }  // ID владельца пропуска
        [Required, StringLength(10)]
        public string? OwnerType { get; set; }  // "Student", "Employee" или "Visitor"
    }
}
