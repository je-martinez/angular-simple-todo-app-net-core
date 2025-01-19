using Microsoft.AspNetCore.Mvc;
using simple_todo_bll.Auth.DTOs;
using simple_todo_bll.Auth.Mappers;
using simple_todo_bll.Auth.Utils;
using simple_todo_bll.Shared.Utils;
using simple_todo_dal;
using simple_todo_database.Context;
using simple_todo_database.Entities;

namespace simple_todo_bll.Auth
{
    public class AuthBLL : IAuthBLL
    {

        private readonly ApiDbContext _context;
        private readonly IJwtUtils _utils;
        public AuthBLL(ApiDbContext context, IJwtUtils utils)
        {
            _context = context;
            _utils = utils;
        }

        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto user)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {

                var userExists = await unitOfWork.UserRepository.Get(u => u.Email == user.Email);
                if (userExists.Count > 0)
                {
                    return new ConflictObjectResult("User already exists");
                }
                var hash = PasswordUtils.generateSalt();
                var passwordHash = PasswordUtils.HashPassword(user.Password, hash);
                var newUser = await unitOfWork.UserRepository.Insert(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = passwordHash,
                    Salt = PasswordUtils.ConvertSaltToString(hash),
                });
                await unitOfWork.Save();
                var result = AuthMappers.ToUserDto(newUser);
                result.Token = _utils.GenerateToken(result);
                return ResponseHelper.Created(string.Empty, result);
            }
        }

        public async Task<ActionResult<UserDto>> Login(LoginUserDto user)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var userExists = await unitOfWork.UserRepository.Get(u => u.Email == user.Email);
                if (userExists.Count == 0)
                {
                    return ResponseHelper.NotFound("User not found");
                }
                var userEntity = userExists[0];
                if (!PasswordUtils.VerifyPassword(
                    user.Password,
                    userEntity.PasswordHash,
                    PasswordUtils.ConvertStringToSalt(userEntity.Salt)))
                {
                    return ResponseHelper.Unauthorized("Invalid password");
                }
                var result = AuthMappers.ToUserDto(userEntity);
                result.Token = _utils.GenerateToken(result);
                return ResponseHelper.Ok(result);
            }
        }
    }
}