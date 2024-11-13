using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dtos
{
    public class CreateCommentDto
    {
        [Required]
        public int PostId {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        [StringLength(100)]
        public string Title {get; set;} = string.Empty;
        [Required]
        public string Content {get; set;} = string.Empty;
    }
}