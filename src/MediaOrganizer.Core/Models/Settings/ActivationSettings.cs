using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class ActivationSettings
    {
        [JsonProperty("is_service_enabled")]
        public bool IsServiceEnabled { get; set; }

        [JsonProperty("service_interval_in_minutes")]
        public int ServiceScanIntervalInMinutes { get; set; }
    }
}
