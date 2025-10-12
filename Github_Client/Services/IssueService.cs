using Github_Client.Models;

namespace Github_Client.Services
{
    public class IssueService
    {
        private readonly HttpService _http;
        public IssueService(HttpService http) => _http = http;

        public Task<List<IssueModel>?> ListIssuesAsync(string owner, string repo, CancellationToken ct = default)
            => _http.GetAsync<List<IssueModel>>($"repos/{owner}/{repo}/issues?per_page=100", ct);

        public Task<IssueModel?> CreateIssueAsync(string owner, string repo, IssueCreateRequest req, CancellationToken ct = default)
            => _http.PostAsync<IssueCreateRequest, IssueModel>($"repos/{owner}/{repo}/issues", req, ct);
    }
}
