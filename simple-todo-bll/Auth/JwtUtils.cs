using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using simple_todo_bll.Auth.DTOs;
using simple_todo_database.Context;

namespace simple_todo_bll.Auth
{
    public class JwtUtils : IJwtUtils
    {

        private readonly ApiDbContext _context;
        private readonly JwtConfigDto _config;
        public JwtUtils(ApiDbContext context, JwtConfigDto config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config.Secret);
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
                Issuer = _config.Issuer,
                Audience = _config.Issuer,
                IssuedAt = DateTime.UtcNow,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

    }
}