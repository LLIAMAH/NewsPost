using NewsPost.Data.Entities;

namespace NewsPost.Models
{
    public class CommentModel
    {
        public string Error { get; set; }
        public Article Article { get; set; }
        public string Text { get; set; }
        public bool IsNull => string.IsNullOrEmpty(Error) && Article == null && string.IsNullOrEmpty(Text);
    }
}
