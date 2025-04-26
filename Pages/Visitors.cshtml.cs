using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using checkpoint.Models;
using checkpoint.Data;

namespace checkpoint.Pages.Visitors
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
