using Github_Client.Models;

namespace Github_Client.Services
{
    public class ActionsService
    {
        private readonly HttpService _http;
        public ActionsService(HttpService http) => _http = http;

        public Task<WorkflowListResponse?> ListWorkflowsAsync(string owner, string repo, CancellationToken ct = default)
            => _http.GetAsync<WorkflowListResponse>($"repos/{owner}/{repo}/actions/workflows", ct);

        public Task<HttpResponseMessage> DispatchWorkflowAsync(string owner, string repo, string workflowFileName, string @ref, object? inputs = null, CancellationToken ct = default)
            => _http.PostAsyncRaw($"repos/{owner}/{repo}/actions/workflows/{workflowFileName}/dispatches",
                new { @ref, inputs }, ct);
    }
}
