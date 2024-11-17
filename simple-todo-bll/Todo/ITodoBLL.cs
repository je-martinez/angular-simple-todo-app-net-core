using simple_todo_bll.Todo.DTOs;

namespace simple_todo_bll.Todo
{
    public interface ITodoBLL
    {
        public Task<List<TodoDto>> GetTodos();
        public Task<TodoDto> GetTodoById(string id);
        public Task<TodoDto> CreateTodo(CreateTodoDto todo);
        public Task<TodoDto> UpdateTodoById(string id, UpdateTodoDto todo);
        public Task<bool> DeleteTodoById(string id);
    }
}