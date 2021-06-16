using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tomato.FinanceMonitor.StratWinConsole
{
    public class LogHelper
    {
        private readonly string logFile = "";
        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        /// <summary>
        /// 不带参数的构造函数
        /// </summary>
        public LogHelper()
        {
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="logFile"></param>
        public LogHelper(string logFile)
        {
            this.logFile = logFile;
        }
        /// <summary>
        /// 追加一条信息
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            try
            {
                LogWriteLock.EnterWriteLock();

                using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 追加一条信息
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="text"></param>
        public void Write(string logFile, string text)
        {
            try
            {
                LogWriteLock.EnterWriteLock();

                using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
        /// <summary>
        /// 追加一行信息
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            try
            {
                LogWriteLock.EnterWriteLock();

                text += "\r\n";
                using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }

        }
        /// <summary>
        /// 追加一行信息
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="text"></param>
        public void WriteLine(string logFile, string text)
        {
            try
            {
                LogWriteLock.EnterWriteLock();

                text += "\r\n";
                using (StreamWriter sw = new StreamWriter(logFile, true, Encoding.UTF8))
                {
                    sw.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + text);
                }
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
    }
}
