using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using TheLibrary_DataAccessLayer.Models;

namespace TheLibrary_BusinessLayer.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(User user, IList<string> roles)
        {
            // Paso 1: Definir los Claims estándar del usuario
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("firstName", user.FirstName),
            new("lastName", user.LastName)
        };

            // Paso 2: Agregar los roles como claims tipados (ClaimTypes.Role)
            // Esto permite que [Authorize(Roles = "...")] evalúe correctamente los permisos
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Paso 3: Obtener y preparar la clave simétrica
            var secretKey = _config["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("La clave secreta de JWT no está configurada.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Paso 4: Calcular la fecha de expiración
            var durationMinutes = Convert.ToDouble(_config["JwtSettings:DurationInMinutes"] ?? "60");
            var expiration = DateTime.UtcNow.AddMinutes(durationMinutes);

            // Paso 5: Construir el token con Header, Payload y Firma
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Paso 6: Serializar el token a su formato compacto (string separado por puntos)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
