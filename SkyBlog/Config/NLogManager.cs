/*
* ==============================================================================
*
* File name: NLogManager
* Description: NLog日志方法提供类
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using NLog;
using System;
namespace SkyBlog.Config
{
    public class NLogManager: ILogManager
    {
        private  Logger logger;

        public NLogManager()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }
       

        public void Error(string msg)
        {
            logger.Error(msg);
        }

        public void Error(string msg, Exception e)
        {
            logger.Error(e,msg);
        }

        public void Error(Exception e)
        {
            if (e is AggregateException || e is OperationCanceledException || e is System.Threading.Tasks.TaskCanceledException)
            {

            }
            else
            {
                logger.Error(e);
            }
        }


        public void Info(string msg)
        {
            logger.Info(msg);
        }

        public void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public void Warning(string msg)
        {
            logger.Warn(msg);
        }

       
    }
}
