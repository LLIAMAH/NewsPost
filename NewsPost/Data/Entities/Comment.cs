using System;

namespace NewsPost.Data.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
