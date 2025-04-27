using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using checkpoint.Models;
using checkpoint.Data;

namespace checkpoint.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public StudentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get; set; } = new List<Student>();

        public async Task OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
        }
    }
}
