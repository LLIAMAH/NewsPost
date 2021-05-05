using System;
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
                _logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var principalName = principal.Identity?.Name;
                return _ctx.Users.SingleOrDefault(o => o.UserName.Equals(principalName));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLog(functionName, ex));
            }
            finally
            {
                _logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }

            return default;
        }

        public string GetUserId(IPrincipal principal)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;

            try
            {
                _logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var user = GetUser(principal);
                return user.Id;
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLog(functionName, ex));
            }
            finally
            {
                _logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }

            return default;
        }
    }
}
