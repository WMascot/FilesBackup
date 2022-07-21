namespace FilesBackup.Models
{
    public class ConfigData
    {
        public string sourceDirectory { get; set; }
        public string destinationDirectory { get; set; }
        public string logLevel { get; set; }
        public string logDirectory { get; set; }
    }
}
