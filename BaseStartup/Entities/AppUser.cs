using Microsoft.AspNetCore.Identity;

namespace BaseStartup.Entities;

public class AppUser : IdentityUser
{
    public string Avatar { get; set; } = String.Empty;
    public string NickName { get; set; } = String.Empty;
}
