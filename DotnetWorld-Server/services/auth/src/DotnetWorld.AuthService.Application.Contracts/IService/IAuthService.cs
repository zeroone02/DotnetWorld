namespace DotnetWorld.AuthService.Application.Contracts;
public interface IAuthService
{
    Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
}
