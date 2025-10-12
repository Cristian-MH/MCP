using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Github_Client.Core
{
    public class MCPClient
    {
        private readonly ILogger<MCPClient> _logger;

        public MCPClient(ILogger<MCPClient> logger)
        {
            _logger = logger;
        }

        public Task SendContextAsync(object context)
        {
            var json = JsonSerializer.Serialize(context, new JsonSerializerOptions { WriteIndented = true });
            _logger.LogInformation("Sending MCP context:\n{json}", json);
            return Task.CompletedTask;
        }
    }
}
