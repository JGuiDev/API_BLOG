using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Interfaces
{
    public interface ICommentRepository
    {
        Task<ResponseModel<List<CommentDto>>> GetAll();
        Task<ResponseModel<CommentDto?>> GetCommentById(int id);
        Task<ResponseModel<Comment>> Comment(CreateCommentDto createCommentDto);
        Task<ResponseModel<Comment?>> Update(int id, UpdateCommentDto updateCommentDto);
        Task<ResponseModel<Comment?>> Delete(int id);
    }
}