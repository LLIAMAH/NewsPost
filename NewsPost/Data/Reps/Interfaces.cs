using System.Security.Principal;
using NewsPost.Data.Entities;

namespace NewsPost.Data.Reps
{
    public interface IRep: IRepUser { }

    public interface IRepUser
    {
        ApplicationUser GetUser(IPrincipal principal);
        string GetUserId(IPrincipal principal);
    }
}
