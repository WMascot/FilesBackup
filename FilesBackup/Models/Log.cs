using FilesBackup.Utils;

namespace FilesBackup.Models
{
    public class Log
    {
        public LogLevel logLevel { get; private set; }
        public string logDirectory { get; private set; }
        public Log(LogLevel logLevel, string logDirectory)
        {
            this.logLevel = logLevel;
            this.logDirectory = logDirectory;
        }
    }
}
