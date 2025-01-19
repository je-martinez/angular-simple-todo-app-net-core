using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth
{
    public interface IJwtUtils
    {
        public string GenerateToken(UserDto user);
        public string GenerateRefreshToken();
    }
}