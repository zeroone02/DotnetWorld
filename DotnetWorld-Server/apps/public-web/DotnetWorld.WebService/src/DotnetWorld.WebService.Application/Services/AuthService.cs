using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;

namespace DotnetWorld.WebService.Application;
public class AuthService : IAuthService
{
    public readonly IHttpClientService _httpClientService;
    public AuthService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.AuthAPIBase + "/api/auth/AssignRole",
            Data = registrationRequestDto
        });
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.AuthAPIBase + "/api/auth/login",
            Data = loginRequestDto
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.AuthAPIBase + "/api/auth/register",
            Data = registrationRequestDto
        });
    }
}
