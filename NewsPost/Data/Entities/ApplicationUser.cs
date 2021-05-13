using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace NewsPost.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("User name")]
        public override string UserName { get; set; }
    }
}
