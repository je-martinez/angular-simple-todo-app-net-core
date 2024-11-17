using simple_todo_bll.Todo.DTOs;

namespace simple_todo_bll
{
    public interface ITodoBLL
    {
        public Task<List<TodoDto>> GetTodos();
        public Task<TodoDto> GetTodoById(string id);
        public Task<TodoDto> CreateTodo(TodoDto todo);
        public Task<TodoDto> UpdateTodoById(string id, TodoDto todo);
        public void DeleteTodoById(string id);
    }
}