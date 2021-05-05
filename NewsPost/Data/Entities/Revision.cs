using System;

namespace NewsPost.Data.Entities
{
    public class Revision
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
