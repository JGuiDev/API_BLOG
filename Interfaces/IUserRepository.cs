using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Interfaces
{
    public interface IUserRepository
    {
        Task<ResponseModel<List<UserDto>>> GetAll();
        Task<ResponseModel<UserDto?>> GetUserById(int id);
        Task<ResponseModel<User>> Register(RegisterUserDto registerUserDto);
        Task<ResponseModel<User?>> Update(int id, UpdateUserDto registerUserDto);
        Task<ResponseModel<User?>> Delete(int id);
    }
}