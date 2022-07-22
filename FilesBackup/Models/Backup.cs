using FilesBackup.Utils;

namespace FilesBackup.Models
{
    public class Backup
    {
        public Config config { get; private set; }
        public Log log { get; private set; }
        public string backupDirectory { get; private set; }
        public int errorsCount { get; private set; }
        public Backup()
        {
            config = new Config();
            log = new Log(config.configData.logLevel, config.configData.logDirectory);
            errorsCount = 0;
        }
        public void StartBackup()
        {
            log.WriteLog("Starting Backup", LogLevel.Info);
            CreateTimeStampedDirectory();
            CopyFilesystem();
        }
        private void CreateTimeStampedDirectory()
        {
            log.WriteLog($"Creating directory with time stamp at {config.configData.destinationDirectory}", LogLevel.Info);
            var directoryName = $"Backup_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm")}";
            var fullPath = Path.Combine(config.configData.destinationDirectory, directoryName);
            try
            {
                Directory.CreateDirectory(fullPath);
                log.WriteLog($"Directory {directoryName} was successfully created", LogLevel.Info);
                backupDirectory = fullPath;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message, LogLevel.Error);
                errorsCount++;
            }

        }
        private void CopyFilesystem()
        {
            if (backupDirectory == null || backupDirectory.Length == 0)
            {
                log.WriteLog($"Backup failed with {errorsCount} Errors", LogLevel.Info);
                return;
            }
        }
    }
}
