using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ArcCorpEmergencyUI.Models;
using Microsoft.JSInterop;

namespace ArcCorpEmergencyUI.Services;

public class ApiChatService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _jsRuntime;

    private const string BaseUrl = "https://arccorpbackendprosustrack-production.up.railway.app/api/users";
    private const string TokenKey = "authToken";

    public ApiChatService(HttpClient http, IJSRuntime jsRuntime)
    {
        _http = http;
        _jsRuntime = jsRuntime;
    }

    public async Task<List<ChatModel>> GetChatsAsync()
    {
        var request = await CreateRequest(HttpMethod.Get, "get_chats");
        return await ExecuteRequest<List<ChatModel>>(request) ?? new();
    }

    public async Task<List<MessageModel>> GetMessagesAsync(string chatId)
    {
        var request = await CreateRequest(HttpMethod.Get, $"get_messages?chatId={chatId}");
        return await ExecuteRequest<List<MessageModel>>(request) ?? new();
    }

    public async Task<ChatResultModel> CreateNewChatAsync()
    {
        var request = await CreateRequest(HttpMethod.Post, "new_chat");
        return await ExecuteRequest<ChatResultModel>(request)
            ?? throw new Exception("Chat creation failed");
    }

    public async Task<UniversalIntentResponseModel?> SendPromptAsync(string chatId, string message)
    {
        var request = await CreateRequest(HttpMethod.Post, "prompt");
        request.Content = JsonContent.Create(new UserPromptParamModel(chatId, message));
        return await ExecuteRequest<UniversalIntentResponseModel>(request);
    }

    // ===== Utilities =====

    private async Task<HttpRequestMessage> CreateRequest(HttpMethod method, string endpoint)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        var request = new HttpRequestMessage(method, $"{BaseUrl}/{endpoint}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return request;
    }

    private async Task<T?> ExecuteRequest<T>(HttpRequestMessage request)
    {
        using var response = await _http.SendAsync(request);

        // Add these checks
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"HTTP Error: {response.StatusCode}");
            return default;
        }

        var content = await response.Content.ReadAsStringAsync(); // Debugging
        if (string.IsNullOrWhiteSpace(content))
        {
            Console.WriteLine("Empty response received");
            return default;
        }

        try
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Error: {ex.Message}\nResponse: {content}");
            return default;
        }
    }
}
