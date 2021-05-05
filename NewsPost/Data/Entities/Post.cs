using System;
using System.Collections.Generic;

namespace NewsPost.Data.Entities
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual ICollection<Revision> Revisions { get; set; }
    }
}
