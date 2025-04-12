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

        public IList<Visitor> Visitors { get; set; } = null!;

        public async Task OnGetAsync()
        {
            Visitors = await _context.Visitors.ToListAsync();
        }
    }
}
