using System.Text.Json.Serialization;

namespace Github_Client.Models
{
    public class PullRequestModel
    {
        [JsonPropertyName("number")] public int Number { get; set; }
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("state")] public string? State { get; set; }
        [JsonPropertyName("html_url")] public string? HtmlUrl { get; set; }
        [JsonPropertyName("user")] public UserModel? User { get; set; }
        [JsonPropertyName("head")] public BranchRef? Head { get; set; }
        [JsonPropertyName("base")] public BranchRef? Base { get; set; }
    }

    public class BranchRef
    {
        [JsonPropertyName("label")] public string? Label { get; set; }
        [JsonPropertyName("ref")] public string? Ref { get; set; }
        [JsonPropertyName("sha")] public string? Sha { get; set; }
    }

    public class PullRequestCreateRequest
    {
        [JsonPropertyName("title")] public string Title { get; set; } = default!;
        [JsonPropertyName("head")] public string Head { get; set; } = default!;
        [JsonPropertyName("base")] public string Base { get; set; } = default!;
        [JsonPropertyName("body")] public string? Body { get; set; }
        [JsonPropertyName("draft")] public bool Draft { get; set; } = false;
    }
}
