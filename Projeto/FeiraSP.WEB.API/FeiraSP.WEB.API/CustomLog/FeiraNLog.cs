using NLog;

namespace FeiraSP.WEB.API.CustomLog
{
    public class FeiraNLog: IFeiraLog
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public FeiraNLog()
        {
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
