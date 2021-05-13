using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NewsPost.Areas.Admin.Models;
using NewsPost.Data.LogData;

namespace NewsPost.Data.Reps
{
    public partial class Rep
    {
        public IResult<IEnumerable<UserData>> GetUserWithRoles()
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));

                var users = this._ctx.Users.ToList();
                var userRoles = this._ctx.UserRoles.ToList();
                var roles = this._ctx.Roles.ToList();

                var result = new List<UserData>();
                foreach (var user in users)
                {
                    var userData = new UserData(user);
                    var ur = userRoles.Where(o => o.UserId.Equals(user.Id)).ToList();
                    if (ur.Any())
                    {
                        foreach (var role in ur.Select(userRole => 
                            roles
                                .SingleOrDefault(o => o.Id.Equals(userRole.RoleId)))
                            .Where(role => role != null))
                            userData.AddRole(role);
                    }

                    result.Add(userData);
                }

                return new Result<IEnumerable<UserData>>(result);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return new Result<IEnumerable<UserData>>(ex.Message);
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
