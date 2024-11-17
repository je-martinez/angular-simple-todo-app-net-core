
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace simple_todo_api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }

    public class Todo
    {
        [Key]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required bool IsComplete { get; set; }
        public DateTime? CompletedAt { get; set; }

        public required DateTime CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public bool Status { get; set; }
    }
}
