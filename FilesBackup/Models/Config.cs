using FilesBackup.Utils;
using Newtonsoft.Json;

namespace FilesBackup.Models
{
    public class Config
    {
        private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        public ConfigData configData { get; private set; }
        public Config()
        {
            if (File.Exists(_configPath))
            {
                using(var file = File.OpenText(_configPath))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    try
                    {
                        configData = (ConfigData)jsonSerializer.Deserialize(file, typeof(ConfigData))!;
                        ValidateData();
                    }
                    catch (JsonSerializationException e) { throw new JsonSerializationException($"Serialization ended with Errors: {e.Message}"); }
                }
            }
            else throw new FileNotFoundException($"File doesn't exist: {_configPath}");
        }
        private void ValidateData()
        {
            if (!Directory.Exists(configData.sourceDirectory)) throw new DirectoryNotFoundException($"Directory doesn't exist: {configData.sourceDirectory}");
            if (!Directory.Exists(configData.destinationDirectory)) throw new DirectoryNotFoundException($"Directory doesn't exist: {configData.destinationDirectory}");
            if (!Directory.Exists(configData.logDirectory)) throw new DirectoryNotFoundException($"Directory doesn't exist: {configData.logDirectory}");
        }
    }
}