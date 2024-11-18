using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Auth;
using simple_todo_bll.Auth.DTOs;
using simple_todo_database.Entities;

namespace simple_todo_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthBLL _authBLL;

    public AuthController(ILogger<AuthController> logger, IAuthBLL todoBLL)
    {
        _logger = logger;
        _authBLL = todoBLL;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto user)
        => await _authBLL.CreateUser(user);

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserDto user)
        => await _authBLL.Login(user);

}
