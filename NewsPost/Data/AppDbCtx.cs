using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsPost.Data.Entities;

namespace NewsPost.Data
{
    public class AppDbCtx : IdentityDbContext<ApplicationUser>
    {
        public AppDbCtx(DbContextOptions<AppDbCtx> options)
            : base(options)
        {
        }
    }
}
