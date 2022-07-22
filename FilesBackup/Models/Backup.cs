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
            log.WriteLog("End of backup", LogLevel.Info);
            log.WriteLog($"Backup ended with {errorsCount} Errors", LogLevel.Debug);
        }
        private void CreateTimeStampedDirectory()
        {
            log.WriteLog($"Creating directory with time stamp at {config.configData.destinationDirectory}", LogLevel.Info);
            var directoryName = $"Backup_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm")}";
            var fullPath = Path.Combine(config.configData.destinationDirectory, directoryName);
            try
            {
                Directory.CreateDirectory(fullPath);
                log.WriteLog($"Directory {directoryName} was successfully created", LogLevel.Debug);
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
                errorsCount++;
                log.WriteLog($"Backup failed with {errorsCount} Errors", LogLevel.Error);
                return;
            }
            DirectoryInfo sourceDirectory = new DirectoryInfo(config.configData.sourceDirectory);
            DirectoryInfo destinationDirectory = new DirectoryInfo(backupDirectory);
            CopyDirectory(sourceDirectory, destinationDirectory);
        }
        private void CopyDirectory(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory)
        {
            log.WriteLog($"Starting copying directory {sourceDirectory.Name}", LogLevel.Info);
            try
            {
                DirectoryInfo newDirectory = Directory.CreateDirectory(Path.Combine(destinationDirectory.FullName, sourceDirectory.Name));

                try
                {
                    foreach (var file in sourceDirectory.GetFiles())
                    {
                        log.WriteLog($"Starting copying file {file.Name}", LogLevel.Info);
                        file.CopyTo(Path.Combine(newDirectory.FullName, file.Name), false);
                        log.WriteLog($"File {file.Name} was successfully copied", LogLevel.Debug);
                    }
                }
                catch (Exception ex)
                {
                    errorsCount++;
                    log.WriteLog(ex.Message, LogLevel.Error);
                }

                foreach (var subDirectory in sourceDirectory.GetDirectories())
                {
                    CopyDirectory(subDirectory, newDirectory);
                }

                log.WriteLog($"Directory {sourceDirectory.Name} was successfully copied", LogLevel.Debug);
            }
            catch (Exception ex)
            {
                errorsCount++;
                log.WriteLog(ex.Message, LogLevel.Error);
            }
        }
    }
}
