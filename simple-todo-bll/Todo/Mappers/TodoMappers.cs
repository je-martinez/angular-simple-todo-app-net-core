using AutoMapper;
using simple_todo_bll.Todo.DTOs;

namespace simple_todo_bll.Todo.Mappers
{
    public static class TodoMappers
    {
        private static MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<simple_todo_database.Entities.Todo, TodoDto>().ReverseMap();
        });

        public static IMapper Mapper = configuration.CreateMapper();

        public static TodoDto ToTodoDto(this simple_todo_database.Entities.Todo todo)
        {
            return Mapper.Map<TodoDto>(todo);
        }

        public static simple_todo_database.Entities.Todo ToTodoEntity(this TodoDto todo)
        {
            return Mapper.Map<simple_todo_database.Entities.Todo>(todo);
        }
    }
}