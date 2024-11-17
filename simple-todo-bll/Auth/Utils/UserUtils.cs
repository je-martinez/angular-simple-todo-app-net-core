using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth.Utils
{
    public static class UserUtils
    {
        public static UserDto ExtractUserFromRequest(System.Security.Claims.ClaimsPrincipal user)
        {
            return new UserDto
            {
                Id = user.FindFirst("id")?.Value ?? string.Empty,
                Name = user.FindFirst("name")?.Value ?? string.Empty,
                Email = user.FindFirst("e-mail")?.Value ?? string.Empty,
                Status = bool.Parse(user.FindFirst("status")?.Value ?? "true")
            };
        }
    }
}