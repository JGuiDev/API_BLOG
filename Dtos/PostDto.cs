using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<Comment> Comments {get; set;} = new List<Comment>();
    }
}