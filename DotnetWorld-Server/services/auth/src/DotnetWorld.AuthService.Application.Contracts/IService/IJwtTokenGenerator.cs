using DotnetWorld.AuthService.Domain;

namespace DotnetWorld.AuthService.Application.Contracts;
public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser applicationUser);
}
