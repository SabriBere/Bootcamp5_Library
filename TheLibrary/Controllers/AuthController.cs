using Microsoft.AspNetCore.Mvc;
using TheLibrary_BusinessLayer.DTOs;
using TheLibrary_BusinessLayer.Services.Auth;

namespace TheLibrary_PresentationLayer.Controllers
{
    public class AuthController : ControllerBase
    {

        // Inyección de dependencias del servicio de autenticación
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            // Condición para verificar si el registro fue exitoso
            if (string.IsNullOrEmpty(response.Token))
            {
                return BadRequest(new { Message = "El registro falló. Por favor, verifica los datos proporcionados." });
            }
            return Ok(response);

        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            // Condición para verificar si el inicio de sesión fue exitoso
            if (string.IsNullOrEmpty(response.Token))
            {
                return BadRequest(new { Message = "El inicio de sesión falló. Por favor, verifica tus credenciales." });
            }
            return Ok(response);
        }
    }
}
