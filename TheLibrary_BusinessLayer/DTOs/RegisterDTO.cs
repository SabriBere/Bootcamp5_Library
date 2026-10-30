using System;
using System.Collections.Generic;
using System.Text;

namespace TheLibrary_BusinessLayer.DTOs
{
    // Entrada para el registro de un nuevo usuario
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Rol del usuario (por ejemplo, "Admin", "User", etc.)
    }
}
