using System.ComponentModel.DataAnnotations;

namespace NewsPost.Models
{
    public interface IPost
    {
        long Id { get; set; }
        string Title { get; set; }
        string Body { get; set; }
    }

    public class PostData: IPost
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
