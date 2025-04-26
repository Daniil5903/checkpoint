using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models;
using Microsoft.EntityFrameworkCore;
using checkpoint.Data;

namespace checkpoint.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Visitor> Visitors { get; set; } = new List<Visitor>();
        public async Task OnGetAsync()
        {
            Visitors = await _context.Visitors.ToListAsync();
        }
    }
}
