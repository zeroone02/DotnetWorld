namespace DotnetWorld.AuthService.Application.Contracts;
public interface IAuthService
{
    Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
    Task<LoginResponseDto> Register(LoginRequestDto loginRequestDto);
}
