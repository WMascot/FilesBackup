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
            if (logLevel == LogLevel.None) return;
            var logName = $"Log_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}.txt";
            var fullPath = Path.Combine(logDirectory, logName);
            try
            {
                logWriter = File.CreateText(fullPath);
            }
            catch (Exception ex) { throw new Exception($"Failed creating log file: {ex.Message}"); }
        }
        public void WriteLog(string message, LogLevel level)
        {
            if (logLevel == LogLevel.None) return;
            if (logLevel < level) return;
            logWriter.WriteLine($"{DateTime.Now} {level} " + message);
            logWriter.Flush();
        }
    }
}
