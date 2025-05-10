using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace checkpoint.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;

        // Добавляем SignInManager в конструктор
        public RegisterModel(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;  // Инициализация SignInManager
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string ConfirmPassword { get; set; } = null!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new AuthUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                // Используем _signInManager для аутентификации пользователя
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index"); // перенаправление после успешной регистрации
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
