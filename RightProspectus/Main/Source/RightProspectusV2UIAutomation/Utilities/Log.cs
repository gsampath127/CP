using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace Utilities
{
    public static class Log
    {
        private static string sourcePath = ConfigurationManager.AppSettings["SourceFolder"].ToString();
        private static string logPath = Path.Combine(sourcePath, "log", DateTime.Now.ToString("MMddyyyy") + ".csv");
        private static ReaderWriterLockSlim logLock = new ReaderWriterLockSlim();
        public static void WriteLog(string message)
        {
            logLock.EnterWriteLock();
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine(message);
                sw.Close();
            }
            logLock.ExitWriteLock();
        }
    }
}
