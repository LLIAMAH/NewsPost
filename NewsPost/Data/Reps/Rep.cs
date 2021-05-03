using Microsoft.Extensions.Logging;

namespace NewsPost.Data.Reps
{
    public partial class Rep : IRepUsers
    {
        private readonly AppDbCtx _ctx;
        private readonly ILogger<AppDbCtx> _logger;

        public Rep(AppDbCtx ctx, ILogger<AppDbCtx> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }
    }
}
