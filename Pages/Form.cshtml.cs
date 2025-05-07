using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using checkpoint.Data;
using checkpoint.Models;
using Microsoft.AspNetCore.Mvc;

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

        [BindProperty] public int? EditingPassId { get; set; }
        [BindProperty] public string? EditedPurpose { get; set; }
        [BindProperty] public string? EditedSurname { get; set; }
        [BindProperty] public string? EditedName { get; set; }
        [BindProperty] public string? EditedPatronymic { get; set; }
        [BindProperty] public string? EditedPosition { get; set; }

        public class PassViewModel
        {
            public int PassId { get; set; }
            public int OwnerId { get; set; }
            public string OwnerType { get; set; } = "";
            public string FullName { get; set; } = "";
            public string Surname { get; set; } = "";
            public string Name { get; set; } = "";
            public string Patronymic { get; set; } = "";
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
                string surname = "", name = "", patronymic = "";

                switch (pass.OwnerType)
                {
                    case "Student":
                        var student = await _context.Students.FindAsync(pass.OwnerId);
                        if (student != null)
                        {
                            surname = student.Surname;
                            name = student.Name;
                            patronymic = student.Patronymic;
                            fullName = $"{surname} {name} {patronymic}";
                        }
                        break;

                    case "Employee":
                        var employee = await _context.Employees.FindAsync(pass.OwnerId);
                        if (employee != null)
                        {
                            surname = employee.Surname;
                            name = employee.Name;
                            patronymic = employee.Patronymic;
                            fullName = $"{surname} {name} {patronymic}";
                            position = employee.Position;
                        }
                        break;

                    case "Visitor":
                        var visitor = await _context.Visitors.FindAsync(pass.OwnerId);
                        if (visitor != null)
                        {
                            surname = visitor.Surname;
                            name = visitor.Name;
                            patronymic = visitor.Patronymic;
                            fullName = $"{surname} {name} {patronymic}";
                        }
                        break;
                }

                Passes.Add(new PassViewModel
                {
                    PassId = pass.Id,
                    OwnerId = pass.OwnerId,
                    OwnerType = pass.OwnerType,
                    FullName = fullName,
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    Purpose = pass.Purpose,
                    IssueDate = pass.IssueDate,
                    Position = position
                });
            }
        }

        public async Task<IActionResult> OnPostEditAsync(int passId)
        {
            EditingPassId = passId;
            await OnGetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync(int passId)
        {
            var pass = await _context.Passes.FindAsync(passId);
            if (pass == null)
                return RedirectToPage();

            if (!string.IsNullOrWhiteSpace(EditedPurpose))
                pass.Purpose = EditedPurpose;

            switch (pass.OwnerType)
            {
                case "Student":
                    var student = await _context.Students.FindAsync(pass.OwnerId);
                    if (student != null)
                    {
                        student.Surname = EditedSurname ?? student.Surname;
                        student.Name = EditedName ?? student.Name;
                        student.Patronymic = EditedPatronymic ?? student.Patronymic;
                    }
                    break;

                case "Employee":
                    var employee = await _context.Employees.FindAsync(pass.OwnerId);
                    if (employee != null)
                    {
                        employee.Surname = EditedSurname ?? employee.Surname;
                        employee.Name = EditedName ?? employee.Name;
                        employee.Patronymic = EditedPatronymic ?? employee.Patronymic;
                        if (!string.IsNullOrWhiteSpace(EditedPosition))
                            employee.Position = EditedPosition;
                    }
                    break;

                case "Visitor":
                    var visitor = await _context.Visitors.FindAsync(pass.OwnerId);
                    if (visitor != null)
                    {
                        visitor.Surname = EditedSurname ?? visitor.Surname;
                        visitor.Name = EditedName ?? visitor.Name;
                        visitor.Patronymic = EditedPatronymic ?? visitor.Patronymic;
                    }
                    break;
            }

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
