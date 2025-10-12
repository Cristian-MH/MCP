using Github_Client.Models;

namespace Github_Client.Services
{
    public class PullRequestService
    {
        private readonly HttpService _http;
        public PullRequestService(HttpService http) => _http = http;

        public Task<List<PullRequestModel>?> ListPullRequestsAsync(string owner, string repo, CancellationToken ct = default)
            => _http.GetAsync<List<PullRequestModel>>($"repos/{owner}/{repo}/pulls?per_page=100", ct);

        public Task<PullRequestModel?> CreatePullRequestAsync(string owner, string repo, PullRequestCreateRequest req, CancellationToken ct = default)
            => _http.PostAsync<PullRequestCreateRequest, PullRequestModel>($"repos/{owner}/{repo}/pulls", req, ct);
    }
}
