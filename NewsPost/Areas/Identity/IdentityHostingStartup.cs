using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NewsPost.Areas.Identity.IdentityHostingStartup))]
namespace NewsPost.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}