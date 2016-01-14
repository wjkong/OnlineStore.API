using Kong.OnlineStoreAPI.Model;
using NLog;
using System;

namespace Kong.OnlineStoreAPI.Logic
{
    public class NLogLogger : ILogMgr
    {
        private Logger logger;

        public NLogLogger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Error(Exception ex)
        {
            logger.Error(ex);
        }

        public void Fatal(Exception ex)
        {
            logger.Fatal(ex);
        }

        public void Info(string meg)
        {
            logger.Info(meg);
        }
    }
}
