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
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepo.GetAll();

            if(comments.Status == false)
            {
                return BadRequest(comments);
            }

            return Ok(comments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentId(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.GetCommentById(id);

            if(comment.Status == false)
            {
                return BadRequest(comment);
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CreateCommentDto createCommentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.Comment(createCommentDto);

            if(comment.Status == false)
            {
                return BadRequest(comment.Message);
            }

            return StatusCode(201, comment);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto updateCommentDto)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.Update(id, updateCommentDto);

            if(comment.Status == false)
            {
                return BadRequest(comment);
            }

            return Ok(comment);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepo.Delete(id);

            if(comment.Status == false)
            {
                return BadRequest(comment);
            }

            return NoContent();

        }
    }
}