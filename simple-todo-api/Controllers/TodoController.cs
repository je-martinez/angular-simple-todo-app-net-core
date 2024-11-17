using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using simple_todo_bll.Todo;
using simple_todo_database.Context;
using simple_todo_database.Entities;

namespace simple_todo_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoBLL _todoBLL;


    public TodoController(ILogger<TodoController> logger, ITodoBLL todoBLL)
    {
        _logger = logger;
        _todoBLL = todoBLL;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Todo>>> Get()
    {

        var todos = await _todoBLL.GetTodos();
        return Ok(todos);
    }
}
