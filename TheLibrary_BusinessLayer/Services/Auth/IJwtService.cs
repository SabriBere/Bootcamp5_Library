using System;
using System.Collections.Generic;
using System.Text;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services.Auth
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user, IList<string> roles);
    }
}
