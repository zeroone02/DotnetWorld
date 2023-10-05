using DotnetWorld.WebService.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace DotnetWorld.WebService.Application;
public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void ClearToken()
    {
        throw new NotImplementedException();
    }

    public string? GetToken()
    {
        throw new NotImplementedException();
    }

    public void SetToken(string token)
    {
        throw new NotImplementedException();
    }
}
