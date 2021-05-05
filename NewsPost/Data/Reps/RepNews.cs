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
        public IResult<IEnumerable<News>> GetNewsDaily(DateTime date)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;

            try
            {
                _logger?.LogInformation(LogRecord.CreateLogStart(functionName));

                var news = _ctx.News
                    .Include(o => o.Author)
                    .Where(o => o.DateCreated.Date == date.Date).ToList();

                return news.Any()
                    ? new Result<IEnumerable<News>>(news)
                    : new Result<IEnumerable<News>>("There is no data.");
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
