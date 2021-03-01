using System;
using System.ComponentModel.DataAnnotations;

namespace NewsPost.Data.Entities
{
    public class Post
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
