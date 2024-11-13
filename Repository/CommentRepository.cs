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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CommentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel<Comment>> Comment(CreateCommentDto createCommentDto)
        {
            ResponseModel<Comment> response = new ResponseModel<Comment>();

            try
            {

                var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(p => p.Id == createCommentDto.PostId);

                if(post == null)
                {
                    response.Message = "Post not found here!";

                    return response;
                }

                var comment = new Comment()
                {
                    PostId = createCommentDto.PostId,
                    UserId = createCommentDto.UserId,
                    Title = createCommentDto.Title,
                    Content = createCommentDto.Content,
                    PostedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                _context.Add(comment);
                await _context.SaveChangesAsync();

                var commentMapping = _mapper.Map<Comment>(comment);

                response.Data = commentMapping;
                response.Message = "Comment published with successful!";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<Comment?>> Delete(int id)
        {
            ResponseModel<Comment?> response = new ResponseModel<Comment?>();

            try
            {
                var comment = await _context.Posts.FirstOrDefaultAsync(c => c.Id == id);

                if(comment == null)
                {
                    response.Message = "Comment not found in the system!";

                    return response;
                }

                _context.Remove(comment);
                await _context.SaveChangesAsync();

                var commentMapping = _mapper.Map<Comment>(comment);

                response.Data = commentMapping;
                response.Message = "Comment deleted with successful.";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<CommentDto>>> GetAll()
        {
            ResponseModel<List<CommentDto>> response = new ResponseModel<List<CommentDto>>();

            try
            {
                var comments = await _context.Comments.ToListAsync();

                var commentsMapping = _mapper.Map<List<CommentDto>>(comments);

                response.Data = commentsMapping;
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<CommentDto?>> GetCommentById(int id)
        {
            ResponseModel<CommentDto?> response = new ResponseModel<CommentDto?>();
            
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

                if(comment == null)
                {
                    response.Message = "Comment not found in the system!";

                    return response;
                }

                var commentMapping = _mapper.Map<CommentDto>(comment);

                response.Data = commentMapping;
                response.Message = "Comment located in the system!";
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<Comment?>> Update(int id, UpdateCommentDto updateCommentDto)
        {
             ResponseModel<Comment?> response = new ResponseModel<Comment?>();

            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);

                if(comment == null)
                {
                    response.Message = "Comment not found in the system!";

                    return response;
                }

                comment.Title = updateCommentDto.Title;
                comment.Content = updateCommentDto.Content;
                comment.ModifiedAt = DateTime.Now;

                _context.Update(comment);
                await _context.SaveChangesAsync();

                var commentMapping = _mapper.Map<Comment>(comment);

                response.Data = commentMapping;
                response.Message = "Comment updated with successful";

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