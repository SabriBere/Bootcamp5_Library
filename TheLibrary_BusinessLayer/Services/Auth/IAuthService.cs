using TheLibrary_BusinessLayer.DTOs;
namespace TheLibrary_BusinessLayer.Services.Auth;
public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
}