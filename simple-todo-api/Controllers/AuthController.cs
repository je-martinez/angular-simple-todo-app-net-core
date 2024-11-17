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
    public async Task<ActionResult<List<Todo>>> Register([FromBody] CreateUserDto user)
    {
        var newUser = await _authBLL.CreateUser(user);
        return Ok(newUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<List<Todo>>> Login([FromBody] LoginUserDto user)
    {
        var loggedUser = await _authBLL.Login(user);
        return Ok(loggedUser);
    }
}
