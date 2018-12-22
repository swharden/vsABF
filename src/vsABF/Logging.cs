using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsABF
{
    public enum LogLevel { DEBUG, INFO, WARN, CRITICAL };
    public class Logging
    {
        public LogLevel logLevel;
        public bool silent = false;
        public string logText = "";
        public Logging(LogLevel logLevel = LogLevel.DEBUG) { this.logLevel = logLevel; }
        public void Debug(string message) { Log(message, LogLevel.DEBUG); }
        public void Info(string message) { Log(message, LogLevel.INFO); }
        public void Warn(string message) { Log(message, LogLevel.WARN); }
        public void Critical(string message) { Log(message, LogLevel.CRITICAL); }
        private void Log(string message, LogLevel logLevel)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] logLevelNames = { "DEBUG", "INFO", "WARN", "CRITICAL" };
            string logLevelName = logLevelNames[(int)logLevel];
            string logLine = $"[{timestamp}] ({logLevelName}): {message}";
            logText = logText + logLine + "\n";
            if (!this.silent & logLevel>=this.logLevel)
                System.Console.WriteLine(logLine);
        }
    }
}
