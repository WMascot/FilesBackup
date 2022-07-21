using FilesBackup.Utils;

namespace FilesBackup.Models
{
    public class ConfigData
    {
        public string sourceDirectory { get; set; }
        public string destinationDirectory { get; set; }
        public LogLevel logLevel { get; set; }
        public string logDirectory { get; set; }
    }
}
