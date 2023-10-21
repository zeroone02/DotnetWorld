using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

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
        _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
        if (hasToken == true)
        {
            return token;
        }
        else
        {
            return null;
        }
    }

    public void SetToken(string token)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
    }
}
