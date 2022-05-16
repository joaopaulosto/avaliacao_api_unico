using NLog;

namespace FeiraSP.WEB.API.CustomLog
{
    public class FeiraNLog: IFeiraLog
    {
        private NLog.ILogger _logger;


        public FeiraNLog() : this (null)
        {
            
        }
        public FeiraNLog(NLog.ILogger logIntance)
        {
            if (logIntance == null)
                _logger = LogManager.GetCurrentClassLogger();
            else
                _logger = logIntance;
        }

        public void Information(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

       
    }
}
