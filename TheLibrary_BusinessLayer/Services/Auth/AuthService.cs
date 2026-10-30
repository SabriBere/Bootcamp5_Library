using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_DataAccessLayer.Models;
using TheLibrary_BusinessLayer.DTOs;

namespace TheLibrary_BusinessLayer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        public AuthService(IJwtService jwtService, 
            UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {
            // Verificar si el correo electrónico ya está registrado
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDTO { Token = null };
            }

            // Instancia del usuario a registrar
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return new AuthResponseDTO { Token = null };
            }
            // Asignar el rol en la tabla de roles
            await _userManager.AddToRoleAsync(user, "User");
            var token = _jwtService.GenerateJwtToken(user, new List<string> { "User" });
            return new AuthResponseDTO { Token = token };
        }

        // Método para el inicio de sesión
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            // Verificar si el usuario existe y si la contraseña es correcta
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthResponseDTO { Token = null };
            }
            var roles = await _userManager.GetRolesAsync(user);

            // Generar el token JWT para el usuario autenticado
            var token = _jwtService.GenerateJwtToken(user, roles);
            return new AuthResponseDTO { Token = token };
        }

    }
}