using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Todo.DTOs;

namespace simple_todo_bll.Todo
{
    public interface ITodoBLL
    {
        public Task<ActionResult<List<TodoDto>>> GetTodos();
        public Task<ActionResult<TodoDto>> GetTodoById(string id);
        public Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto todo);
        public Task<ActionResult<TodoDto>> UpdateTodoById(string id, UpdateTodoDto todo);
        public Task<ActionResult> DeleteTodoById(string id);
    }
}