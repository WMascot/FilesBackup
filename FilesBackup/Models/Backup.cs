using FilesBackup.Models;

namespace FilesBackup.Models
{
    public class Backup
    {
        public Config config { get; private set; }
        public Log log { get; private set; }
        public Backup()
        {
            config = new Config();
            log = new Log(config.configData.logLevel, config.configData.logDirectory);
        }
        public void StartBackup()
        {
            CreateTimeStampedDirectory();
            CopyFilesystem();
        }
        private void CreateTimeStampedDirectory()
        {

        }
        private void CopyFilesystem()
        {

        }
    }
}
