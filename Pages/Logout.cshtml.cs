using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace checkpoint.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AuthUser> _signInManager;

        public LogoutModel(SignInManager<AuthUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _signInManager.SignOutAsync();  // Выход пользователя
            return RedirectToPage("/Index"); // Перенаправление после выхода
        }
    }
}
