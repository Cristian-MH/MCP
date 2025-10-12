using System.Text.Json.Serialization;

namespace Github_Client.Models
{
    public class RepositoryModel
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("html_url")] public string? HtmlUrl { get; set; }
        [JsonPropertyName("visibility")] public string? Visibility { get; set; }
        [JsonPropertyName("private")] public bool Private { get; set; }
        [JsonPropertyName("owner")] public UserModel? Owner { get; set; }
    }

    public class RepoCreateRequest
    {
        [JsonPropertyName("name")] public string Name { get; set; } = default!;
        [JsonPropertyName("description")] public string? Description { get; set; }
        [JsonPropertyName("private")] public bool Private { get; set; } = true;
        [JsonPropertyName("auto_init")] public bool AutoInit { get; set; } = true;
    }
}
