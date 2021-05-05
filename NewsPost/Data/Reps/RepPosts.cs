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
        public IResult<IEnumerable<Article>> GetUnApprovedArticles(string userId)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var articles = this._ctx.Articles
                    .Include(o => o.Author)
                    .Where(o => o.DateApproved == null)
                    .ToList();

                if (!articles.Any())
                    throw new Exception("There is no data.");

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

        public IResult<bool> Create(Article article)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));

                if (article == null)
                    throw new ArgumentNullException(nameof(article));

                this._ctx.Articles.Add(article);
                return SaveSafe(this._ctx);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return new Result<bool>(ex.Message);
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
