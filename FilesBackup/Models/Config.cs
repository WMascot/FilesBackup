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
                    }
                    catch (JsonSerializationException) { throw new JsonSerializationException("Serialization ended with errors"); }
                }
            }
            else throw new FileNotFoundException($"File doesn't exist at path: {_configPath}");
        }
    }
}