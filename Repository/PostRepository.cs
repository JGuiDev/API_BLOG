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
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PostRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ResponseModel<Post?>> Delete(int id)
        {
            ResponseModel<Post?> response = new ResponseModel<Post?>();

            try
            {
                var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(u => u.Id == id);

                if(post == null)
                {
                    response.Message = "Post not found in the system!";

                    return response;
                }

                _context.Remove(post);
                await _context.SaveChangesAsync();

                var postMapping = _mapper.Map<Post>(post);

                response.Data = postMapping;
                response.Message = "Post deleted with successful.";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<List<PostDto>>> GetAll()
        {
            ResponseModel<List<PostDto>> response = new ResponseModel<List<PostDto>>();

            try
            {
                var posts = await _context.Posts.Include(c => c.Comments).ToListAsync();

                var postsMapping = _mapper.Map<List<PostDto>>(posts);

                response.Data = postsMapping;
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<PostDto?>> GetPostById(int id)
        {
            ResponseModel<PostDto?> response = new ResponseModel<PostDto?>();
            
            try
            {
                var user = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(u => u.Id == id);

                if(user == null)
                {
                    response.Message = "Post not found in the system!";

                    return response;
                }

                var postMapping = _mapper.Map<PostDto>(user);

                response.Data = postMapping;
                response.Message = "Post located in the system!";
                
                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<Post>> Post(CreatePostDto createPostDto)
        {
             ResponseModel<Post> response = new ResponseModel<Post>();

            try
            {
                var post = new Post()
                {
                    UserId = createPostDto.UserId,
                    Title = createPostDto.Title,
                    Content = createPostDto.Content,
                    Topic = createPostDto.Topic,
                    PostedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                _context.Add(post);
                await _context.SaveChangesAsync();

                var postMapping = _mapper.Map<Post>(post);

                response.Data = postMapping;
                response.Message = "Post published with successful!";

                return response;
            }
            catch(Exception error)
            {
                response.Message = error.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ResponseModel<Post?>> Update(int id, UpdatePostDto updatePostDto)
        {
            ResponseModel<Post?> response = new ResponseModel<Post?>();

            try
            {
                var post = await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(u => u.Id == id);

                if(post == null)
                {
                    response.Message = "Post not found in the system!";

                    return response;
                }

                post.UserId = updatePostDto.UserId;
                post.Title = updatePostDto.Title;
                post.Content = updatePostDto.Content;
                post.Topic = updatePostDto.Topic;
                post.ModifiedAt = DateTime.Now;

                _context.Update(post);
                await _context.SaveChangesAsync();

                var postMapping = _mapper.Map<Post>(post);

                response.Data = postMapping;
                response.Message = "Post updated with successful";

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