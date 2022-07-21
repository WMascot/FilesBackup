using FilesBackup.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FilesBackup.Models
{
    public class ConfigData
    {
        [JsonProperty(Required = Required.Always)]
        public string sourceDirectory { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string destinationDirectory { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevel logLevel { get; set; } = LogLevel.None;
        public string logDirectory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
    }
}
