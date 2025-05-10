using checkpoint.Data;
using checkpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace checkpoint.Pages
{
    [Authorize]
    public class StudentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StudentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = new();

        [BindProperty]
        public string Purpose { get; set; } = "";

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Добавляем студента
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            // Создаем пропуск
            var pass = new Pass
            {
                PassNumber = $"S-{Student.Id:D4}",
                PassType = "Student",
                IssueDate = DateTime.Now,
                ExpirationDate = null,
                Purpose = Purpose,
                IsActive = true,
                OwnerId = Student.Id,
                OwnerType = "Student"
            };

            _context.Passes.Add(pass);
            await _context.SaveChangesAsync();

            return RedirectToPage("Form");
        }
    }
}
