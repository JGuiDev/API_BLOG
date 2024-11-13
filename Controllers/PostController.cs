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
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepo;

        public PostController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepo.GetAll();

            if(posts.Status == false)
            {
                return BadRequest(posts.Message);
            }

            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPost(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepo.GetPostById(id);

            if(post.Status == false)
            {
                return BadRequest(post.Message);
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepo.Post(createPostDto);

            if(post.Status == false)
            {
                return BadRequest(post.Message);
            }

            return StatusCode(201, post);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepo.Update(id, updatePostDto);

            if(post.Status == false)
            {
                return BadRequest(post.Message);
            }

            return Ok(post);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _postRepo.Delete(id);

            if(post.Status == false)
            {
                return BadRequest(post.Message);
            }

            return NoContent();
        }
    }
}