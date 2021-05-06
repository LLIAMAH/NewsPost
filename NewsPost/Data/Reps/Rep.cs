using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using NewsPost.Data.LogData;

namespace NewsPost.Data.Reps
{
    public partial class Rep : IRep
    {
        private readonly AppDbCtx _ctx;
        private readonly ILogger<Rep> _logger;

        public Rep(AppDbCtx ctx, ILogger<Rep> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }

        private IResult<bool> SaveSafe(AppDbCtx ctx)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                ctx.SaveChanges();
                return new Result<bool>(true);
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
