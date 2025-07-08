using System.Text.Json.Serialization;

namespace ArcCorpEmergencyUI.Models
{
    public class CoreResultModel
    {
        [JsonPropertyName("sucessful")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
