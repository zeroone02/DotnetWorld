
using Microsoft.AspNetCore.Identity;

namespace DotnetWorld.AuthService.Domain;
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}
