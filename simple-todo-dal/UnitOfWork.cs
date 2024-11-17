using simple_todo_database.Context;
using simple_todo_database.Entities;

namespace simple_todo_dal
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApiDbContext _context;
        private GenericRepository<Todo> _todoRepository;

        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Todo> TodoRepository
        {
            get
            {
                if (_todoRepository == null)
                {
                    _todoRepository = new GenericRepository<Todo>(_context);
                }
                return _todoRepository;
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}