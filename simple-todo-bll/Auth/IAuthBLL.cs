using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth
{
    public interface IAuthBLL
    {
        public Task<ActionResult<UserDto>> CreateUser(CreateUserDto user);
        public Task<ActionResult<UserDto>> Login(LoginUserDto user);
    }
}