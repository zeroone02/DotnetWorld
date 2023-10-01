using DotnetWorld.AuthService.Application.Contracts;
using DotnetWorld.AuthService.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace DotnetWorld.AuthService.Application;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _jwtOptions;
    public JwtTokenGenerator(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    public string GenerateToken(ApplicationUser applicationUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
    }
    //
}
