using Microsoft.Extensions.Logging;

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
    }
}
