using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAll();

            if(users.Status == false)
            {
                return BadRequest(users.Message);
            }

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetUserById(id);

            if(user.Status == false)
            {
                return BadRequest(user.Message);
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.Register(registerUserDto);

            if(user.Status == false)
            {
                return BadRequest(user.Message);
            }

            return StatusCode(201, user);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.Update(id, updateUserDto);

            if(user.Status == false)
            {
                return BadRequest(user.Message);
            }

            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.Delete(id);

            if(user.Status == false)
            {
                return BadRequest(user.Message);
            }

            return NoContent();
        }
    }
}