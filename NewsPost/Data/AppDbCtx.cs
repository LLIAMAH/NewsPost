using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsPost.Data.Entities;

namespace NewsPost.Data
{
    public class AppDbCtx : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Revision> Revisions { get; set; }

        public AppDbCtx(DbContextOptions<AppDbCtx> options)
            : base(options)
        {
        }
    }
}
