using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPost.Data.Entities
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("Article")]
        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
