using System.Text.Json.Serialization;

namespace Github_Client.Models
{
    public class IssueModel
    {
        [JsonPropertyName("number")] public int Number { get; set; }
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("html_url")] public string? HtmlUrl { get; set; }
        [JsonPropertyName("state")] public string? State { get; set; }
        [JsonPropertyName("user")] public UserModel? User { get; set; }
    }

    public class IssueCreateRequest
    {
        [JsonPropertyName("title")] public string Title { get; set; } = default!;
        [JsonPropertyName("body")] public string? Body { get; set; }
        [JsonPropertyName("assignees")] public List<string>? Assignees { get; set; }
        [JsonPropertyName("labels")] public List<string>? Labels { get; set; }
    }
}
