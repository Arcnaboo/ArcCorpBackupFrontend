using System.Text.Json.Serialization;

namespace ArcCorpEmergencyUI.Models
{
    public class LoginResultModel 
    {
        [JsonPropertyName("sucessful")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("jwtAuthKey")]
        public string JwtAuthKey { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}

