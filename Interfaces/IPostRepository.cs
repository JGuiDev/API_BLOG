using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Interfaces
{
    public interface IPostRepository
    {
        Task<ResponseModel<List<PostDto>>> GetAll();
        Task<ResponseModel<PostDto?>> GetPostById(int id);
        Task<ResponseModel<Post>> Post(CreatePostDto createPostDto);
        Task<ResponseModel<Post?>> Update(int id, UpdatePostDto updatePostDto);
        Task<ResponseModel<Post?>> Delete(int id);
    }
}