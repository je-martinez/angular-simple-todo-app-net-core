
using Microsoft.EntityFrameworkCore;
using simple_todo_api.Entities;

namespace simple_todo_api.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }


}
