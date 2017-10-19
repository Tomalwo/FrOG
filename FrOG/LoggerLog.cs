using System;
using System.IO;

namespace FrOG
{
    internal class LoggerLog
    {
        private readonly StreamWriter _log;

        public LoggerLog(string logPath)
        {
            _log = new StreamWriter(logPath, false);
        }

        private void LogNewLine(string line, bool bolFlush)
        {
            _log.WriteLine(line);
            if (bolFlush) _log.Flush();
        }

        public void LogLoggerLine(string docName, string parameters, double value)
        {
            LogNewLine(docName + DateTime.Now.ToString(" hh:mm:ss") + " Parameters " + parameters + " Objective " + value, true);
        }

        public void CloseLog()
        {
            _log.Close();
        }
    }
}

