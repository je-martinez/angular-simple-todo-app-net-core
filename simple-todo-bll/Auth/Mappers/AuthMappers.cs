using AutoMapper;
using simple_todo_bll.Auth.DTOs;
using simple_todo_bll.Todo.DTOs;

namespace simple_todo_bll.Auth.Mappers
{
    public static class AuthMappers
    {
        private static MapperConfiguration configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<simple_todo_database.Entities.User, UserDto>().ReverseMap();
        });

        public static IMapper Mapper = configuration.CreateMapper();

        public static UserDto ToUserDto(this simple_todo_database.Entities.User user)
        {
            return Mapper.Map<UserDto>(user);
        }
    }
}