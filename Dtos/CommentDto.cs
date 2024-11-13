using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int PostId {get; set;}
        public int UserId {get; set;}
        public string Title {get; set;} = string.Empty;
        public string Content {get; set;} = string.Empty;
        public DateTime PostedAt {get; set;}
        public DateTime ModifiedAt {get; set;}
    }
}