using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models; 
using checkpoint.Data;   

namespace checkpoint.Pages
{
    public class FormModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public FormModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Student Student { get; set; } = null!;
        [BindProperty]
        public Employee Employee { get; set; } = null!;
        [BindProperty]
        public Visitor Visitor { get; set; } = null!;
        public void OnGet()
        {
        }
        public IActionResult OnPostStudent()
        {
            if (!ModelState.IsValid)
                return Page();
            _context.Students.Add(Student);
            _context.SaveChanges();
            return RedirectToPage("/Student");
        }
        public IActionResult OnPostEmployee()
        {
            if (!ModelState.IsValid)
                return Page();
            _context.Employees.Add(Employee);
            _context.SaveChanges();

            return RedirectToPage("/Employees");
        }
        public IActionResult OnPostVisitor()
        {
            if (!ModelState.IsValid)
                return Page();
            _context.Visitors.Add(Visitor);
            _context.SaveChanges();
            return RedirectToPage("/Visitors/Index");
        }
    }
}
