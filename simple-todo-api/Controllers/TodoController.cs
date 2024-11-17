using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Todo;
using simple_todo_bll.Todo.DTOs;
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
    public async Task<ActionResult<List<Todo>>> GetAll()
    {

        var todos = await _todoBLL.GetTodos();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(string id)
    {

        var todo = await _todoBLL.GetTodoById(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }


    [HttpPost()]
    public async Task<ActionResult<Todo>> CreateTodo([FromBody] CreateTodoDto newTodo)
    {

        var todo = await _todoBLL.CreateTodo(newTodo);
        return Ok(todo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> UpdateTodo(string id, [FromBody] UpdateTodoDto updatedTodo)
    {

        var todo = await _todoBLL.UpdateTodoById(id, updatedTodo);
        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteById(string id)
    {

        _todoBLL.DeleteTodoById(id);
        return NoContent();
    }
}
