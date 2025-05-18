using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ProfileModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public ProfileModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public IList<string> Roles { get; set; } = new List<string>();

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
}
