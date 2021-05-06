using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPost.Data.Entities
{
    public class Article
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Sub title")]
        public string SubTitle { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }

        [ForeignKey("AuthorId")]
        public string AuthorId { get; set; }
        [Required]
        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("ApprovedBy")]
        public string ApprovedById { get; set; }
        public virtual ApplicationUser ApprovedBy { get; set; }
    }
}
