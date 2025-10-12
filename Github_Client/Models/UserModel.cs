using System.Text.Json.Serialization;

namespace Github_Client.Models
{
    public class UserModel
    {
        [JsonPropertyName("login")] public string? Login { get; set; }
        [JsonPropertyName("html_url")] public string? HtmlUrl { get; set; }
    }
}
