using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using checkpoint.Data;
using checkpoint.Models;

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

        [BindProperty, Required(ErrorMessage = "Цель обязательна")]
        public string? EditedPurpose { get; set; }

        [BindProperty, Required(ErrorMessage = "Фамилия обязательна")]
        public string? EditedSurname { get; set; }

        [BindProperty, Required(ErrorMessage = "Имя обязательно")]
        public string? EditedName { get; set; }

        [BindProperty]
        public string? EditedPatronymic { get; set; }

        [BindProperty]
        [RegularExpression("Преподаватель|Директор|Уборщик|Охранник", ErrorMessage = "Недопустимая должность")]
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
                        var employee = await _context.Employees.FindAsync(pass.OwnerId);
                        if (employee != null)
                        {
                            fullName = $"{employee.Surname} {employee.Name} {employee.Patronymic}";
                            position = employee.Position;
                        }
                        break;

                    case "Visitor":
                        var visitor = await _context.Visitors.FindAsync(pass.OwnerId);
                        if (visitor != null)
                            fullName = $"{visitor.Surname} {visitor.Name} {visitor.Patronymic}";
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

        public async Task<IActionResult> OnPostEditAsync(int passId)
        {
            EditingPassId = passId;

            var pass = await _context.Passes.FindAsync(passId);
            if (pass == null) return NotFound();

            switch (pass.OwnerType)
            {
                case "Student":
                    var student = await _context.Students.FindAsync(pass.OwnerId);
                    if (student != null)
                    {
                        EditedSurname = student.Surname;
                        EditedName = student.Name;
                        EditedPatronymic = student.Patronymic;
                    }
                    break;

                case "Employee":
                    var employee = await _context.Employees.FindAsync(pass.OwnerId);
                    if (employee != null)
                    {
                        EditedSurname = employee.Surname;
                        EditedName = employee.Name;
                        EditedPatronymic = employee.Patronymic;
                        EditedPosition = employee.Position;
                    }
                    break;

                case "Visitor":
                    var visitor = await _context.Visitors.FindAsync(pass.OwnerId);
                    if (visitor != null)
                    {
                        EditedSurname = visitor.Surname;
                        EditedName = visitor.Name;
                        EditedPatronymic = visitor.Patronymic;
                    }
                    break;
            }

            EditedPurpose = pass.Purpose;

            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync(int passId)
        {
            if (!ModelState.IsValid)
            {
                EditingPassId = passId;
                await OnGetAsync();
                return Page();
            }

            var pass = await _context.Passes.FindAsync(passId);
            if (pass == null) return NotFound();

            switch (pass.OwnerType)
            {
                case "Student":
                    var student = await _context.Students.FindAsync(pass.OwnerId);
                    if (student != null)
                    {
                        student.Surname = EditedSurname!;
                        student.Name = EditedName!;
                        student.Patronymic = EditedPatronymic;
                    }
                    break;

                case "Employee":
                    var employee = await _context.Employees.FindAsync(pass.OwnerId);
                    if (employee != null)
                    {
                        employee.Surname = EditedSurname!;
                        employee.Name = EditedName!;
                        employee.Patronymic = EditedPatronymic;
                        employee.Position = EditedPosition;
                    }
                    break;

                case "Visitor":
                    var visitor = await _context.Visitors.FindAsync(pass.OwnerId);
                    if (visitor != null)
                    {
                        visitor.Surname = EditedSurname!;
                        visitor.Name = EditedName!;
                        visitor.Patronymic = EditedPatronymic;
                    }
                    break;
            }

            pass.Purpose = EditedPurpose!;
            await _context.SaveChangesAsync();
            return RedirectToPage();
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
    }
}
