using Microsoft.Extensions.Logging;
using Github_Client.Models;

namespace Github_Client.Services
{
    public class GitHubApiService
    {
        private readonly HttpService _http;
        private readonly ILogger<GitHubApiService> _logger;

        public GitHubApiService(HttpService http, ILogger<GitHubApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public Task<List<RepositoryModel>?> GetRepositoriesAsync(CancellationToken ct = default)
            => _http.GetAsync<List<RepositoryModel>>("user/repos?per_page=100", ct);

        public Task<RepositoryModel?> CreateRepositoryAsync(RepoCreateRequest req, CancellationToken ct = default)
            => _http.PostAsync<RepoCreateRequest, RepositoryModel>("user/repos", req, ct);

        public Task<HttpResponseMessage> DeleteRepositoryAsync(string owner, string repo, CancellationToken ct = default)
            => _http.DeleteAsync($"repos/{owner}/{repo}", ct);
    }
}
