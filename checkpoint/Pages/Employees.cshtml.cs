using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models;
using checkpoint.Data;

namespace checkpoint.Pages
{
    public class EmployeesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EmployeesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; } = new();

        [BindProperty]
        public string Purpose { get; set; } = "";

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Добавляем сотрудника
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            // Создаем пропуск
            var pass = new Pass
            {
                PassNumber = $"S-{Employee.Id:D4}",
                PassType = "Employee",
                IssueDate = DateTime.Now,
                ExpirationDate = null,
                Purpose = Purpose,
                IsActive = true,
                OwnerId = Employee.Id,
                OwnerType = "Employee"
            };

            _context.Passes.Add(pass);
            await _context.SaveChangesAsync();

            return RedirectToPage("Form");
        }
    }
}
