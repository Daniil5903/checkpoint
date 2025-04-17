using checkpoint.Data;
using checkpoint.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace checkpoint.Pages
{
    public class EmployeesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EmployeesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get; set; } = null!;

        public async Task OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync();
        }

    }
}
