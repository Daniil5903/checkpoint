using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
                    FullName = fullName,
                    OwnerType = pass.OwnerType,
                    Purpose = pass.Purpose,
                    IssueDate = pass.IssueDate,
                    Position = position
                });
            }
        }

        public class PassViewModel
        {
            public string FullName { get; set; } = "";
            public string? OwnerType { get; set; }
            public string? Purpose { get; set; }
            public DateTime IssueDate { get; set; }
            public string? Position { get; set; }
        }
    }

}
