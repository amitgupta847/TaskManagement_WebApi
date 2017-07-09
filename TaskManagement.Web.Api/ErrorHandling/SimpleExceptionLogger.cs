using System.Web.Http.ExceptionHandling;
using log4net;
using TaskManagement.Common;

namespace TaskManagement.Web.Api
{
    public class SimpleExceptionLogger : ExceptionLogger
    {
        private readonly ILog _log;
        public SimpleExceptionLogger(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(SimpleExceptionLogger));
        }
        public override void Log(ExceptionLoggerContext context)
        {
            _log.Error("Unhandled exception", context.Exception);
        }
    }
}