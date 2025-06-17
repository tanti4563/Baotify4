using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    using System;
    using System.Configuration;
    using System.IO;
    public class LoggingHelper
    {
        private string logFolderPath;
        private static object logFileLocker = new Object();
        public LoggingHelper()
        {
            /*
            this.FilePath = System.Configuration.ConfigurationManager.AppSettings["filePath"]+"Logs/";
            this.FileNm = "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
             * */

            string sLogFolderPath = ConfigurationManager.AppSettings["LOG_FOLDER"];

            sLogFolderPath = PathFileHelper.GetFolderAbsolutePath(sLogFolderPath);

            if (!Directory.Exists(sLogFolderPath))
            {
                Directory.CreateDirectory(sLogFolderPath);
            }

            logFolderPath = sLogFolderPath;
        }

        public void WriteLog(string Message)
        {
            lock (logFileLocker)
            {
                string sLogFilePath = Path.Combine(logFolderPath, "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");

                using (StreamWriter sw = new StreamWriter(sLogFilePath, true))
                {
                    using (TextWriter tw = TextWriter.Synchronized(sw))
                    {
                        tw.WriteLine(string.Format("{0}\t{1}\n", DateTime.Now.ToString(), string.Format(Message)));
                    }
                }
            }
        }
    }
}
