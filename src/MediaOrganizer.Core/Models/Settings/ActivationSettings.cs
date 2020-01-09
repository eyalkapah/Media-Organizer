using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class ActivationSettings
    {
        [JsonProperty("service_interval_in_minutes")]
        public int ServiceScanIntervalInMinutes { get; set; }
    }
}
