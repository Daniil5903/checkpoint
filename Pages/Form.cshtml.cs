using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models; // Замени на свой namespace с моделями
using checkpoint.Data;   // Замени на свой namespace с DbContext

namespace checkpoint.Pages
{
    public class FormModel : PageModel
    {
        private readonly AppDbContext _context;

        public FormModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public Visitor Visitor { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostStudent()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Students.Add(Student);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostEmployee()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Employees.Add(Employee);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostVisitor()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Visitors.Add(Visitor);
            _context.SaveChanges();

            return RedirectToPage();
        }
    }
}
