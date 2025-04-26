using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models;
using checkpoint.Data;
using Microsoft.EntityFrameworkCore;

namespace checkpoint.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public StudentsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Student> Students { get; set; } = new List<Student>();
        public async Task OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
        }
    }
}
