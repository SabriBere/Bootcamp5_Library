using TheLibrary_DataAccessLayer.Models;
namespace TheLibrary_BusinessLayer.Services.Auth;

public interface IJwtService
{
    string  GenerateToken(User user, IList<string> roles);
}