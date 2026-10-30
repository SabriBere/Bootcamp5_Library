using System;
using System.Collections.Generic;
using System.Text;

// Respuesta enviada al cliente después de un login exitoso
namespace TheLibrary_BusinessLayer.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } // Token JWT generado
        public string Email { get; set; } // Email del usuario autenticado
        public string UserId { get; set; } // Id del usuario autenticado
    }
}
