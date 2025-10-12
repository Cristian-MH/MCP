using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Github_Client.Services
{
    public class HttpService
    {
        private readonly HttpClient _http;
        private readonly ILogger<HttpService> _logger;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

        public HttpService(HttpClient http, ILogger<HttpService> logger)
        {
            _http = http;
            _logger = logger;

            _http.BaseAddress = new Uri("https://api.github.com/");
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("GithubMCPClient/1.0");

            var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("Missing GITHUB_TOKEN environment variable.");

            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (outcome, timespan, retryAttempt, context) =>
                    {
                        _logger.LogWarning("Delaying for {delay}s, then making retry {retry}.", timespan.TotalSeconds, retryAttempt);
                    });
        }

        public async Task<T?> GetAsync<T>(string path, CancellationToken ct = default)
        {
            var resp = await _retryPolicy.ExecuteAsync(() => _http.GetAsync(path, ct));
            await EnsureSuccessWithContext(resp);
            return await resp.Content.ReadFromJsonAsync<T>(cancellationToken: ct);
        }

        public async Task<TOut?> PostAsync<TIn, TOut>(string path, TIn body, CancellationToken ct = default)
        {
            var resp = await _retryPolicy.ExecuteAsync(() => _http.PostAsJsonAsync(path, body, ct));
            await EnsureSuccessWithContext(resp);
            return await resp.Content.ReadFromJsonAsync<TOut>(cancellationToken: ct);
        }

        public async Task<HttpResponseMessage> PostAsyncRaw<TIn>(string path, TIn body, CancellationToken ct = default)
        {
            var resp = await _retryPolicy.ExecuteAsync(() => _http.PostAsJsonAsync(path, body, ct));
            await EnsureSuccessWithContext(resp);
            return resp;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path, CancellationToken ct = default)
        {
            var resp = await _retryPolicy.ExecuteAsync(() => _http.DeleteAsync(path, ct));
            await EnsureSuccessWithContext(resp);
            return resp;
        }

        private async Task EnsureSuccessWithContext(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode) return;

            var details = await resp.Content.ReadAsStringAsync();
            var remaining = resp.Headers.TryGetValues("X-RateLimit-Remaining", out var r) ? r.FirstOrDefault() : "?";
            var reset = resp.Headers.TryGetValues("X-RateLimit-Reset", out var rs) ? rs.FirstOrDefault() : "?";

            _logger.LogError("HTTP {status}. RateLimit Remaining={remaining}, Reset={reset}. Body={body}",
                (int)resp.StatusCode, remaining, reset, details);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new InvalidOperationException("Unauthorized. Check your GITHUB_TOKEN scopes.");
            if ((int)resp.StatusCode == 429)
                throw new InvalidOperationException("Hit GitHub rate limit. Try again later.");

            resp.EnsureSuccessStatusCode();
        }
    }
}
