using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace checkpoint.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        public RegisterModel(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public InputModel Input { get; set; } = new();
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var user = new AuthUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                // Если пользователь с таким email уже существует, получаем его.
                var existingUser = await _userManager.FindByEmailAsync(Input.Email);
                if (existingUser != null)
                {
                    // Если это админ, то назначаем роль админа, иначе роль User
                    if (Input.Email == "ssuueee1@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(existingUser, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(existingUser, "User");
                    }
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToPage("/Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }

    }
}
