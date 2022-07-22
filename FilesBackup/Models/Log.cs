using FilesBackup.Utils;

namespace FilesBackup.Models
{
    public class Log
    {
        public LogLevel logLevel { get; private set; }
        public string logDirectory { get; private set; }
        public StreamWriter logWriter { get; private set; }
        public Log(LogLevel logLevel, string logDirectory)
        {
            this.logLevel = logLevel;
            this.logDirectory = logDirectory;
            logWriter?.Dispose();
            CreateLog();
        }
        private void CreateLog()
        {
            var logName = $"Log_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.txt";
            logWriter = File.CreateText(logDirectory + $"\\{logName}");
        }
        public void WriteLog(string message, LogLevel level)
        {
            if (logLevel < level) return;
            logWriter.WriteLine($"{DateTime.Now} {level} " + message);
            logWriter.Flush();
        }
    }
}
