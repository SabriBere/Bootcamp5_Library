using Microsoft.AspNetCore.Mvc;

using TheLibrary_BusinessLayer.DTOs;
using TheLibrary_BusinessLayer.Services.Auth;

namespace TheLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var response = await _authService.RegisterAsync(registerDTO);

        if (string.IsNullOrWhiteSpace(response.Token))
        {
            return BadRequest(new
            {
                message = "No se pudo registrar el usuario"
            });
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var response = await _authService.LoginAsync(loginDTO);

        if (string.IsNullOrWhiteSpace(response.Token))
        {
            return Unauthorized(new
            {
                message = "Email o contraseña incorrectos"
            });
        }

        return Ok(response);
    }
}