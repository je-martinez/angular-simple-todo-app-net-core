using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Todo;
using simple_todo_bll.Todo.DTOs;

namespace simple_todo_api.Controllers;

[ApiController]
[Authorize]
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
    public async Task<ActionResult<List<TodoDto>>> GetAll() => await _todoBLL.GetTodos();


    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetById(string id) => await _todoBLL.GetTodoById(id);


    [HttpPost()]
    public async Task<ActionResult<TodoDto>> CreateTodo([FromBody] CreateTodoDto newTodo)
        => await _todoBLL.CreateTodo(newTodo);

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoDto>> UpdateTodo(string id, [FromBody] UpdateTodoDto updatedTodo)
        => await _todoBLL.UpdateTodoById(id, updatedTodo);

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(string id)
        => await _todoBLL.DeleteTodoById(id);

}
