using System.Text.Json.Serialization;

namespace Github_Client.Models
{
    public class WorkflowListResponse
    {
        [JsonPropertyName("total_count")] public int TotalCount { get; set; }
        [JsonPropertyName("workflows")] public List<WorkflowModel>? Workflows { get; set; }
    }

    public class WorkflowModel
    {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("state")] public string? State { get; set; }
        [JsonPropertyName("path")] public string? Path { get; set; }
        [JsonPropertyName("html_url")] public string? HtmlUrl { get; set; }
    }
}
