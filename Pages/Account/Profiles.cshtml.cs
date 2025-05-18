using checkpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize(Roles = "Admin")]
public class ProfilesModel : PageModel
{
    private readonly UserManager<AuthUser> _userManager;

    public ProfilesModel(UserManager<AuthUser> userManager)
    {
        _userManager = userManager;
    }

    public List<UserInfo> Users { get; set; } = new();

    public class UserInfo
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }

    public async Task OnGetAsync()
    {
        Users = new();
        foreach (var user in _userManager.Users)
        {
            Users.Add(new UserInfo
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
    }
}
