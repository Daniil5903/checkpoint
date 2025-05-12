using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace checkpoint.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;

        public LoginModel(SignInManager<AuthUser> signInManager, UserManager<AuthUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                // Проверка, если пользователь не найден
                if (user == null)
                {
                    ErrorMessage = "Пользователь не найден.";
                    return Page();
                }
                var roles = await _userManager.GetRolesAsync(user);
                // Проверка, если пользователь администратор
                if (roles.Contains("Admin"))
                {
                    return RedirectToPage("/AdminPage"); // Если админ - редирект на панель администратора
                }
                else
                {
                    return RedirectToPage("/Index"); // Если обычный пользователь - редирект на главную
                }
            }
            ErrorMessage = "Неправильный email или пароль.";
            return Page();
        }
    }
}
