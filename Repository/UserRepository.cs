using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Data;
using Blog.Dtos;
using Blog.Interfaces;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<User?>> Delete(int id)
        {
            ResponseModel<User?> response = new ResponseModel<User?>();

            try
            {
                var user = await _context.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.Id == id);

                if(user == null)
                {
                    response.Message = "User not found in the system!";

                    return response;
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                var userMapping = _mapper.Map<User>(user);

                response.Data = userMapping;
                response.Message = "Account deleted with successful.";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<UserDto>>> GetAll()
        {
            ResponseModel<List<UserDto>> response = new ResponseModel<List<UserDto>>();

            try
            {
                var users = await _context.Users.Include(p => p.Posts).ToListAsync();

                var usersMapping = _mapper.Map<List<UserDto>>(users);

                response.Data = usersMapping;
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<UserDto?>> GetUserById(int id)
        {
            ResponseModel<UserDto?> response = new ResponseModel<UserDto?>();
            
            try
            {
                var user = await _context.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.Id == id);

                if(user == null)
                {
                    response.Message = "User not found in the system!";

                    return response;
                }

                var userMapping = _mapper.Map<UserDto>(user);

                response.Data = userMapping;
                response.Message = "User located on the system!";
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<User>> Register(RegisterUserDto registerUserDto)
        {
            ResponseModel<User> response = new ResponseModel<User>();

            try
            {
                var user = new User()
                {
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    Email = registerUserDto.Email
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                var userMapping = _mapper.Map<User>(user);

                response.Data = userMapping;
                response.Message = "Account created with successful";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<User?>> Update(int id, UpdateUserDto updateUserDto)
        {
            ResponseModel<User?> response = new ResponseModel<User?>();

            try
            {
                var user = await _context.Users.Include(p => p.Posts).FirstOrDefaultAsync(u => u.Id == id);

                if(user == null)
                {
                    response.Message = "User not found in the system!";

                    return response;
                }

                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Email = updateUserDto.Email;

                _context.Update(user);
                await _context.SaveChangesAsync();

                var userMapping = _mapper.Map<User>(user);

                response.Data = userMapping;
                response.Message = "Account updated with successful";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }
    }
}