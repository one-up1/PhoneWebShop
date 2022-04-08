using Newtonsoft.Json;

namespace PhoneWebShop.Domain.Models.Configuration
{
    public class AppSettings
    {
        [JsonProperty("FileLoggerOutputLocation")]
        public virtual string FileLoggerOutputLocation { get; set; }
        
        [JsonProperty("ConnectionString")]
        public virtual string ConnectionString { get; set; }
    }
}
