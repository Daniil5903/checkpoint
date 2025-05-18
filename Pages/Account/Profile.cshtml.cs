using checkpoint.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class ProfileModel : PageModel
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly SignInManager<AuthUser> _signInManager;
    public ProfileModel(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public IList<string> Roles { get; set; } = new List<string>();
    [BindProperty]
    public ChangePasswordInputModel ChangePasswordModel { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public bool ShowPasswordForm { get; set; }
    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            Email = user.Email ?? "";
            UserName = user.UserName ?? "";
            Roles = await _userManager.GetRolesAsync(user);
        }
    }
    public async Task<IActionResult> OnPostShowFormAsync()
    {
        ShowPasswordForm = true;
        await OnGetAsync(); // Перезагрузка данных профиля
        return Page();
    }
    public async Task<IActionResult> OnPostChangePasswordAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        ShowPasswordForm = true;
        if (!ModelState.IsValid || user == null)
        {
            await OnGetAsync();
            return Page();
        }
        var result = await _userManager.ChangePasswordAsync(user,
            ChangePasswordModel.OldPassword,
            ChangePasswordModel.NewPassword);

        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["Success"] = "Пароль успешно изменён.";
            return RedirectToPage();
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        await OnGetAsync();
        return Page();
    }
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Пароль должен быть не менее 4 символов.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Подтвердите новый пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}
