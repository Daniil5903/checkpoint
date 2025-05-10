using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using checkpoint.Data;
using checkpoint.Models;
using System.ComponentModel.DataAnnotations;

namespace checkpoint.Pages
{
    public class FormModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FormModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PassViewModel> Passes { get; set; } = new();

        [BindProperty]
        public int? EditingPassId { get; set; }

        [BindProperty, Required(ErrorMessage = "Введите фамилию")]
        [MinLength(2, ErrorMessage = "Фамилия должна содержать минимум 2 символа")]
        [MaxLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        [RegularExpression(@"^[А-Яа-яЁёA-Za-z\s\-]+$", ErrorMessage = "Допустимы только буквы, пробел и дефис")]
        public string EditedSurname { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Имя должно содержать минимум 2 символа")]
        [MaxLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        [RegularExpression(@"^[А-Яа-яЁёA-Za-z\s\-]+$", ErrorMessage = "Допустимы только буквы, пробел и дефис")]
        public string EditedName { get; set; } = "";

        [BindProperty]
        [MaxLength(50, ErrorMessage = "Отчество не должно превышать 50 символов")]
        [RegularExpression(@"^[А-Яа-яЁёA-Za-z\s\-]*$", ErrorMessage = "Допустимы только буквы, пробел и дефис")]
        public string? EditedPatronymic { get; set; }

        [BindProperty, Required(ErrorMessage = "Введите цель посещения")]
        [MinLength(3, ErrorMessage = "Цель должна содержать минимум 3 символа")]
        [MaxLength(100, ErrorMessage = "Цель не должна превышать 100 символов")]
        public string? EditedPurpose { get; set; }

        [BindProperty]
        public string? EditedPosition { get; set; }

        public class PassViewModel
        {
            public int PassId { get; set; }
            public string FullName { get; set; } = "";
            public string? OwnerType { get; set; }
            public string? Purpose { get; set; }
            public DateTime IssueDate { get; set; }
            public string? Position { get; set; }
        }

        public async Task OnGetAsync()
        {
            await LoadPassesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int passId)
        {
            var pass = await _context.Passes.FindAsync(passId);
            if (pass != null)
            {
                _context.Passes.Remove(pass);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int passId)
        {
            EditingPassId = passId;

            var pass = await _context.Passes.FindAsync(passId);
            if (pass != null)
            {
                switch (pass.OwnerType)
                {
                    case "Student":
                        var student = await _context.Students.FindAsync(pass.OwnerId);
                        if (student != null)
                        {
                            EditedSurname = student.Surname ?? "";
                            EditedName = student.Name ?? "";
                            EditedPatronymic = student.Patronymic;
                        }
                        break;

                    case "Employee":
                        var emp = await _context.Employees.FindAsync(pass.OwnerId);
                        if (emp != null)
                        {
                            EditedSurname = emp.Surname ?? "";
                            EditedName = emp.Name ?? "";
                            EditedPatronymic = emp.Patronymic;
                            EditedPosition = emp.Position;
                        }
                        break;

                    case "Visitor":
                        var vis = await _context.Visitors.FindAsync(pass.OwnerId);
                        if (vis != null)
                        {
                            EditedSurname = vis.Surname ?? "";
                            EditedName = vis.Name ?? "";
                            EditedPatronymic = vis.Patronymic;
                        }
                        break;
                }

                EditedPurpose = pass.Purpose;
            }

            await LoadPassesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync(int passId)
        {
            if (!ModelState.IsValid)
            {
                EditingPassId = passId;
                await LoadPassesAsync();
                return Page();
            }

            var pass = await _context.Passes.FindAsync(passId);
            if (pass == null) return RedirectToPage();

            pass.Purpose = EditedPurpose;

            switch (pass.OwnerType)
            {
                case "Student":
                    var student = await _context.Students.FindAsync(pass.OwnerId);
                    if (student != null)
                    {
                        student.Surname = EditedSurname;
                        student.Name = EditedName;
                        student.Patronymic = EditedPatronymic;
                    }
                    break;
                case "Employee":
                    var emp = await _context.Employees.FindAsync(pass.OwnerId);
                    if (emp != null)
                    {
                        emp.Surname = EditedSurname;
                        emp.Name = EditedName;
                        emp.Patronymic = EditedPatronymic;
                        emp.Position = EditedPosition;
                    }
                    break;
                case "Visitor":
                    var vis = await _context.Visitors.FindAsync(pass.OwnerId);
                    if (vis != null)
                    {
                        vis.Surname = EditedSurname;
                        vis.Name = EditedName;
                        vis.Patronymic = EditedPatronymic;
                    }
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        private async Task LoadPassesAsync()
        {
            var allPasses = await _context.Passes
                .OrderByDescending(p => p.IssueDate)
                .ToListAsync();

            Passes = new List<PassViewModel>();

            foreach (var pass in allPasses)
            {
                string fullName = "Неизвестно";
                string? position = null;

                switch (pass.OwnerType)
                {
                    case "Student":
                        var student = await _context.Students.FindAsync(pass.OwnerId);
                        if (student != null)
                            fullName = $"{student.Surname} {student.Name} {student.Patronymic}";
                        break;

                    case "Employee":
                        var emp = await _context.Employees.FindAsync(pass.OwnerId);
                        if (emp != null)
                        {
                            fullName = $"{emp.Surname} {emp.Name} {emp.Patronymic}";
                            position = emp.Position;
                        }
                        break;

                    case "Visitor":
                        var vis = await _context.Visitors.FindAsync(pass.OwnerId);
                        if (vis != null)
                            fullName = $"{vis.Surname} {vis.Name} {vis.Patronymic}";
                        break;
                }

                Passes.Add(new PassViewModel
                {
                    PassId = pass.Id,
                    FullName = fullName,
                    OwnerType = pass.OwnerType,
                    Purpose = pass.Purpose,
                    IssueDate = pass.IssueDate,
                    Position = position
                });
            }
        }
    }
}
