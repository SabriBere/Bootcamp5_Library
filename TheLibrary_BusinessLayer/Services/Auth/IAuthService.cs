public interface IAuthService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
}