/*
* ==============================================================================
*
* File name: ILogManager
* Description: 日志接口
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using System;
namespace SkyBlog.Config
{
    public interface ILogManager
    {
        
        void Error(string msg);
        void Error(string msg, Exception e);
        void Error(Exception e);
        void Info(string msg);
        void Debug(string msg);
        void Warning(string msg);
    }
}
