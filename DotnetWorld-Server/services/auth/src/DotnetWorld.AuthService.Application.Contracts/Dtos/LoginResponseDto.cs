namespace DotnetWorld.AuthService.Application.Contracts;
public class LoginResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}
