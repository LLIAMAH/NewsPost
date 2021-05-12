using NewsPost.Data.Entities;

namespace NewsPost.Areas.Admin.Models
{
    public class UserRoles
    {
        public ApplicationUser User { get; set; }
        public string Roles { get; set; }
    }
}
