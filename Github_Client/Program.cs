using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Github_Client.Core;
using Github_Client.Services;
using Github_Client.Models;

var services = new ServiceCollection();

services.AddLogging(b => b.AddSimpleConsole(o => o.TimestampFormat = "[HH:mm:ss] "));
services.AddHttpClient<HttpService>();
services.AddTransient<MCPClient>();
services.AddTransient<GitHubApiService>();
services.AddTransient<IssueService>();
services.AddTransient<PullRequestService>();
services.AddTransient<ActionsService>();

var provider = services.BuildServiceProvider();
var logger = provider.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 GitHub MCP Client started");

// Simple command runner (see bottom of this file for examples)
await CommandRunner.RunAsync(provider, args);

logger.LogInformation("✅ Execution finished");

// ------------ local command runner ------------
static class CommandRunner
{
    public static async Task RunAsync(ServiceProvider provider, string[] args)
    {
        var git = provider.GetRequiredService<GitHubApiService>();
        var issues = provider.GetRequiredService<IssueService>();
        var prs = provider.GetRequiredService<PullRequestService>();
        var actions = provider.GetRequiredService<ActionsService>();
        var mcp = provider.GetRequiredService<MCPClient>();

        if (args.Length == 0)
        {
            Console.WriteLine("Usage examples:");
            Console.WriteLine("  list-repos");
            Console.WriteLine("  create-repo <name> [private:true|false]");
            Console.WriteLine("  delete-repo <owner> <repo>");
            Console.WriteLine("  issues <owner> <repo>");
            Console.WriteLine("  new-issue <owner> <repo> \"title\" \"body\"");
            Console.WriteLine("  prs <owner> <repo>");
            Console.WriteLine("  new-pr <owner> <repo> <sourceBranch> <targetBranch> \"title\"");
            Console.WriteLine("  workflows <owner> <repo>");
            Console.WriteLine("  dispatch <owner> <repo> <workflowFileName> [ref=main]");
            Console.WriteLine();
            Console.WriteLine("Default demo: list and send to MCP");
            var repos = await git.GetRepositoriesAsync();
            await mcp.SendContextAsync(repos);
            return;
        }

        switch (args[0])
        {
            case "list-repos":
                await mcp.SendContextAsync(await git.GetRepositoriesAsync());
                break;
            case "create-repo":
                {
                    var name = args[1];
                    var isPrivate = args.Length > 2 && bool.TryParse(args[2], out var p) ? p : true;
                    await git.CreateRepositoryAsync(new RepoCreateRequest { Name = name, Private = isPrivate });
                    Console.WriteLine($"Created repo {name} (private={isPrivate})");
                }
                break;
            case "delete-repo":
                await git.DeleteRepositoryAsync(args[1], args[2]);
                Console.WriteLine($"Deleted {args[1]}/{args[2]}");
                break;
            case "issues":
                await mcp.SendContextAsync(await issues.ListIssuesAsync(args[1], args[2]));
                break;
            case "new-issue":
                await issues.CreateIssueAsync(args[1], args[2], new IssueCreateRequest { Title = args[3], Body = args[4] });
                Console.WriteLine("Issue created");
                break;
            case "prs":
                await mcp.SendContextAsync(await prs.ListPullRequestsAsync(args[1], args[2]));
                break;
            case "new-pr":
                await prs.CreatePullRequestAsync(args[1], args[2],
                    new PullRequestCreateRequest { Title = args[5], Head = args[3], Base = args[4] });
                Console.WriteLine("✅ PR created");
                break;
            case "workflows":
                await mcp.SendContextAsync(await actions.ListWorkflowsAsync(args[1], args[2]));
                break;
            case "dispatch":
                {
                    var owner = args[1]; var repo = args[2]; var wf = args[3];
                    var @ref = args.Length > 4 ? args[4] : "main";
                    await actions.DispatchWorkflowAsync(owner, repo, wf, @ref);
                    Console.WriteLine("🚀 Workflow dispatched");
                }
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}
