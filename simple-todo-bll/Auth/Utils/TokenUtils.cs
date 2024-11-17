using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth.Utils
{

    public static class TokenUtils
    {
        public static string GenerateToken(UserDto user, JwtConfigDto config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(config.Secret);
            var claims = new ClaimsIdentity(new[] {
                 new Claim("id", user.Id),
                 new Claim("name", user.Name),
                 new Claim("email", user.Email),
                 new Claim("status", user.Status.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = config.Issuer,
                Audience = config.Issuer,
                IssuedAt = DateTime.UtcNow,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
