//me quede aca...
using Microsoft.AspNetCore.Identity;
using TheLibrary_BusinessLayer.DTOs;
using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtService _jwtService; 

    public AuthService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);

        if(existingUser is not null)
        {
            return new AuthResponseDTO();
        }

        var user = new User
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            Email = registerDTO.Email,
            UserName = registerDTO.Email, //username?
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (!result.Succeeded)
        {
            return new AuthResponseDTO();
        }

        const string roleName = "User";

        if(!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, roleName);

        if (!roleResult.Succeeded)
        {
            return new AuthResponseDTO();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user, roles);

        return new AuthResponseDTO
        {
            Token = token,
            Email = user.Email,
            UserId = user.Id
        };

     }   

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if(user is null)
            {
                return new AuthResponseDTO();
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!validPassword)
            {
                return new AuthResponseDTO();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            return new AuthResponseDTO
            {
                Token = token,
                Email = user.Email,
                UserId = user.Id,
            };
        }
    
}