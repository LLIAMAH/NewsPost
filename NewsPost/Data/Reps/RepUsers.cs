using System;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.Log;

namespace NewsPost.Data.Reps
{
    public partial class Rep
    {
        public ApplicationUser GetUser(IPrincipal principal)
        {
            var name = MethodBase.GetCurrentMethod()?.Name;

            try
            {
                _logger?.LogInformation(LogInfo.CreateLogStart(name));
                var principalName = principal.Identity?.Name;
                return _ctx.Users.SingleOrDefault(o => o.UserName.Equals(principalName));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogInfo.CreateLog(name, ex));
            }
            finally
            {
                _logger?.LogInformation(LogInfo.CreateLogFinish(name));
            }

            return default;
        }

        public string GetUserId(IPrincipal principal)
        {
            var name = MethodBase.GetCurrentMethod()?.Name;

            try
            {
                _logger?.LogInformation(LogInfo.CreateLogStart(name));
                var user = GetUser(principal);
                return user.Id;
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogInfo.CreateLog(name, ex));
            }
            finally
            {
                _logger?.LogInformation(LogInfo.CreateLogFinish(name));
            }

            return default;
        }
    }
}
