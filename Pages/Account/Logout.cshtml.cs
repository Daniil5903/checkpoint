using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using checkpoint.Models;

namespace checkpoint.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AuthUser> _signInManager;
        public LogoutModel(SignInManager<AuthUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
