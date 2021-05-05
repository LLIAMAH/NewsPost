using System;
using System.ComponentModel.DataAnnotations;

namespace NewsPost.Data.Entities
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }
    }
}
