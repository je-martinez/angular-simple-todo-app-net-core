using simple_todo_bll.Todo.DTOs;
using simple_todo_bll.Todo.Mappers;
using simple_todo_dal;
using simple_todo_database.Context;
using Microsoft.AspNetCore.Http;
using simple_todo_bll.Auth.DTOs;
using simple_todo_bll.Auth.Utils;
using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Shared.Utils;

namespace simple_todo_bll.Todo
{
    public class TodoBLL : ITodoBLL
    {

        private readonly ApiDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserDto loggedUser;

        public TodoBLL(ApiDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            loggedUser = UserUtils.ExtractUserFromRequest(_httpContextAccessor.HttpContext.User);
        }

        public async Task<ActionResult<List<TodoDto>>> GetTodos()
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var todos = await unitOfWork.TodoRepository.Get();
                var allTodos = todos.Select(TodoMappers.ToTodoDto).ToList();
                return ResponseHelper.Ok(allTodos);
            }

        }

        public async Task<ActionResult<TodoDto>> GetTodoById(string id)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var todo = await unitOfWork.TodoRepository.GetByID(id);
                if (todo == null)
                {
                    return ResponseHelper.NotFound();
                }
                return ResponseHelper.Ok(TodoMappers.ToTodoDto(todo));
            }
        }

        public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto todo)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var newEntity = TodoMappers.ToTodoEntity(todo);
                newEntity.CreatedAt = DateTime.Now.ToUniversalTime();
                newEntity.CreatedBy = loggedUser.Email;
                var newTodo = await unitOfWork.TodoRepository.Insert(newEntity);
                await unitOfWork.Save();
                return ResponseHelper.Created(string.Empty, TodoMappers.ToTodoDto(newTodo));
            }
        }

        public async Task<ActionResult<TodoDto>> UpdateTodoById(string id, UpdateTodoDto todo)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var entityToUpdate = await unitOfWork.TodoRepository.GetByID(id);
                if (entityToUpdate == null)
                {
                    return ResponseHelper.NotFound();
                }
                entityToUpdate.Name = todo.Name;
                entityToUpdate.Description = todo.Description;
                entityToUpdate.IsComplete = todo.IsComplete;
                if (todo.IsComplete && entityToUpdate.CompletedAt == null)
                {
                    entityToUpdate.CompletedAt = DateTime.Now.ToUniversalTime();
                }
                entityToUpdate.UpdatedAt = DateTime.Now.ToUniversalTime();
                entityToUpdate.UpdatedBy = loggedUser.Email;
                unitOfWork.TodoRepository.Update(entityToUpdate);
                await unitOfWork.Save();
                return ResponseHelper.Ok(TodoMappers.ToTodoDto(entityToUpdate));
            }
        }

        public async Task<ActionResult> DeleteTodoById(string id)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {

                var entityToDelete = await unitOfWork.TodoRepository.GetByID(id);
                if (entityToDelete == null)
                {
                    return ResponseHelper.NotFound();
                }
                unitOfWork.TodoRepository.Delete(entityToDelete);
                await unitOfWork.Save();
                return ResponseHelper.NoContent();
            }
        }
    }
}