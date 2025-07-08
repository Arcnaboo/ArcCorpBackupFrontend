using System;
using System.Text.Json.Serialization;

namespace ArcCorpEmergencyUI.Models
{
    public class ChatModel

    {
        [JsonPropertyName("chatId")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("messages")]
        public List<MessageModel> Messages { get; set; }

   
    }

    public class MessageModel
    {
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("isUserMessage")]
        public bool IsUserMessage { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        
        
    }
 



    public class ChatResultModel
    {
        [JsonPropertyName("chat")]
        public ChatModel ChatModel { get; set; }

        [JsonPropertyName("sucessful")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
