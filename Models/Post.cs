using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Topic { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime PostedAt { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ModifiedAt { get; set; }
        public List<Comment> Comments {get; set;} = new List<Comment>();
    }
}