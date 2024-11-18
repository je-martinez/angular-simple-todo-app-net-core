using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Auth.DTOs;

namespace simple_todo_bll.Auth
{
    public interface IAuthBLL
    {
        public Task<IActionResult> CreateUser(CreateUserDto user);
        public Task<IActionResult> Login(LoginUserDto user);
    }
}