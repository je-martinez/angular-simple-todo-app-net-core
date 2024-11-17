using System.ComponentModel.DataAnnotations;

namespace simple_todo_database.Entities
{
    public class Todo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool Status { get; set; }
    }
}