using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;

namespace NewsPost.Data.Reps
{
    public partial class Rep
    {
        public IResult<IEnumerable<Article>> GetNewsDaily(DateTime dateTime)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var articles = this._ctx.Articles
                    .Include(o => o.Author)
                    .Where(o => o.DateCreated.Date == dateTime.Date && o.DateApproved != null)
                    .ToList();

                if (!articles.Any())
                    throw new Exception("There is no data today.");

                return new Result<IEnumerable<Article>>(articles);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return new Result<IEnumerable<Article>>(ex.Message);
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
