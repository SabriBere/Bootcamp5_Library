using System;
using System.Collections.Generic;
using System.Text;
using TheLibrary_BusinessLayer.DTOs;

namespace TheLibrary_BusinessLayer.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
    }
}
