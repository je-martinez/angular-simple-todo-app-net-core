namespace simple_todo_bll.Todo.DTOs
{
    public class UpdateTodoDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
    }
}