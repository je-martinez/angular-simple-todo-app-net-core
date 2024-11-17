using simple_todo_bll.Todo.DTOs;
using simple_todo_bll.Todo.Mappers;
using simple_todo_dal;
using simple_todo_database.Context;

namespace simple_todo_bll.Todo
{
    public class TodoBLL : ITodoBLL
    {

        private readonly ApiDbContext _context;

        public TodoBLL(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoDto>> GetTodos()
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var todos = await unitOfWork.TodoRepository.Get();
                return todos.Select(todo => TodoMappers.ToTodoDto(todo)).ToList();
            }

        }

        public async Task<TodoDto> GetTodoById(string id)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var todo = await unitOfWork.TodoRepository.GetByID(id);
                return TodoMappers.ToTodoDto(todo);
            }
        }

        public async Task<TodoDto> CreateTodo(CreateTodoDto todo)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var newEntity = TodoMappers.ToTodoEntity(todo);
                newEntity.CreatedAt = DateTime.Now.ToUniversalTime();
                newEntity.CreatedBy = "System";
                var newTodo = await unitOfWork.TodoRepository.Insert(newEntity);
                await unitOfWork.Save();
                return TodoMappers.ToTodoDto(newTodo);
            }
        }

        public async Task<TodoDto> UpdateTodoById(string id, UpdateTodoDto todo)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var entityToUpdate = await unitOfWork.TodoRepository.GetByID(id);
                if (entityToUpdate == null)
                {
                    return null;
                }
                entityToUpdate.Name = todo.Name;
                entityToUpdate.Description = todo.Description;
                entityToUpdate.IsComplete = todo.IsComplete;
                if (todo.IsComplete)
                {
                    entityToUpdate.CompletedAt = DateTime.Now.ToUniversalTime();
                }
                entityToUpdate.UpdatedAt = DateTime.Now.ToUniversalTime();
                entityToUpdate.UpdatedBy = "System";
                unitOfWork.TodoRepository.Update(entityToUpdate);
                await unitOfWork.Save();
                return TodoMappers.ToTodoDto(entityToUpdate);
            }
        }

        public async void DeleteTodoById(string id)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {

                var entityToDelete = await unitOfWork.TodoRepository.GetByID(id);
                if (entityToDelete == null)
                {
                    return;
                }
                unitOfWork.TodoRepository.Delete(entityToDelete);
                await unitOfWork.Save();
            }
        }
    }
}