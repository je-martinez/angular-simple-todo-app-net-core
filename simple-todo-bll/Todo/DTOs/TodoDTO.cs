namespace simple_todo_bll.Todo.DTOs
{
    public class TodoDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool Status { get; set; }
    }
}