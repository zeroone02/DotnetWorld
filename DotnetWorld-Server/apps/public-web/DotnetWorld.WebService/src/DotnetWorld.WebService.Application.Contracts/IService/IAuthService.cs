using DotnetWorld.DDD;

namespace DotnetWorld.WebService.Application.Contracts;
public interface IAuthService
{
    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
    Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);

}
