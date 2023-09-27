﻿using Microsoft.AspNetCore.Mvc;

namespace DotnetWorld.AuthService.HttpApi;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

}
