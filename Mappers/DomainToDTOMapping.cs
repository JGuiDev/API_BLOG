using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Mappers
{
    public class DomainToDTOMapping : Profile
    {

        public DomainToDTOMapping()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Post,PostDto>().ReverseMap();
            CreateMap<Comment,CommentDto>().ReverseMap();
        }
        
    }
}