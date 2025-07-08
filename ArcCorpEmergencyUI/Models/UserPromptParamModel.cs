using System.Text.Json.Serialization;

namespace ArcCorpEmergencyUI.Models
{
    public class UserPromptParamModel
    {
        [JsonPropertyName("chatId")]
        public string ChatId { get; set; }

        [JsonPropertyName("userMessage")]
        public string UserMessage { get; set; }

        public UserPromptParamModel(string chatId, string userMessage)
        {
            ChatId = chatId;
            UserMessage = userMessage;
        }
    }
}
