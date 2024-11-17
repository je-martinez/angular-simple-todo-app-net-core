
using Microsoft.EntityFrameworkCore;
using simple_todo_database.Entities;

namespace simple_todo_database.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
