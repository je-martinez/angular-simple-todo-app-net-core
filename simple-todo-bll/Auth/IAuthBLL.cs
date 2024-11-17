using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth
{
    public interface IAuthBLL
    {
        public Task<UserDto> CreateUser(CreateUserDto user);
        public Task<UserDto> Login(LoginUserDto user);
    }
}