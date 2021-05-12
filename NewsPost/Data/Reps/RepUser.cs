using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;

namespace NewsPost.Data.Reps
{
    public partial class Rep
    {
        public ApplicationUser GetUser(IPrincipal principal)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var user = this._ctx.Users.SingleOrDefault(o => o.UserName.Equals(principal.Identity.Name));
                return user;
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }

        public string GetUserId(IPrincipal principal)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                return GetUser(principal)?.Id;
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }

        public bool HasAdminUser()
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var role = this._ctx.Roles.SingleOrDefault(o => o.Name.Equals(ERole.Administrator.ToString()));
                if (role == null)
                    return false;

                var user = this._ctx.UserRoles.Where(o => o.RoleId.Equals(role.Id));
                return user.Any();
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
