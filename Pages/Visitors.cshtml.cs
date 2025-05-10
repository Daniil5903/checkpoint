using checkpoint.Data;
using checkpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace checkpoint.Pages
{
    [Authorize]
    public class VisitorsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VisitorsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Visitor Visitor { get; set; } = new();

        [BindProperty]
        public string Purpose { get; set; } = "";

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Добавляем студента
            _context.Visitors.Add(Visitor);
            await _context.SaveChangesAsync();

            // Создаем пропуск
            var pass = new Pass
            {
                PassNumber = $"S-{Visitor.Id:D4}",
                PassType = "Visitor",
                IssueDate = DateTime.Now,
                ExpirationDate = null,
                Purpose = Purpose,
                IsActive = true,
                OwnerId = Visitor.Id,
                OwnerType = "Visitor"
            };

            _context.Passes.Add(pass);
            await _context.SaveChangesAsync();

            return RedirectToPage("Form");
        }
    }
}
