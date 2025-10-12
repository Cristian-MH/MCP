# 🧩 GitHub MCP Client

[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)](https://dotnet.microsoft.com/)
[![Language](https://img.shields.io/badge/Language-C%23-blue)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green)](../LICENSE)
[![Status](https://img.shields.io/badge/Status-Active-brightgreen)](https://github.com/)

---

## 📘 Overview
The **GitHub MCP Client** is a **C# project** that connects the **Model Context Protocol (MCP)** with the **GitHub REST and GraphQL APIs**.  
It allows AI systems, developer tools, or automation agents to interact programmatically with GitHub repositories, issues, pull requests, and workflows using MCP-compatible context exchanges.

This project is one of several MCP clients, each designed to bridge **AI reasoning** and **real-world developer tools**.

---

## 🧠 What Is MCP?

**Model Context Protocol (MCP)** defines how AI models and software systems exchange structured context, commands, and events.  
It enables **semantic interoperability** — meaning that AI agents can perform real actions through standardized APIs instead of plain text prompts.

In this project, MCP acts as a **bridge** between:
- AI assistants (or the MCP server)
- The GitHub API (via REST or GraphQL)

---

## ⚙️ Features

| Feature | Description | Status |
|----------|--------------|---------|
| 🔐 **Authentication** | Authenticate via OAuth or Personal Access Token (PAT) | 🟢 Implemented |
| 📂 **Repositories** | List, create, and delete repositories | ⚙️ In Progress |
| 🧩 **Issues & PRs** | Manage issues and pull requests | 🕒 Planned |
| 🚀 **GitHub Actions** | Execute and monitor workflows | 🕒 Planned |
| 📊 **MCP Context Models** | Provide structured data for MCP | 🟢 Planned |
| 🧠 **AI Insights** | Summarize repository state for AI assistants | 🕒 Future |

---

## 🏗️ Project Structure

```plaintext
Github_Client/
├── Github_Client.csproj
│
├── /Models
│   ├── RepositoryModel.cs
│   ├── IssueModel.cs
│   ├── UserModel.cs
│
├── /Services
│   ├── GitHubApiService.cs
│   ├── AuthenticationService.cs
│   └── HttpService.cs
│
├── /Core
│   ├── MCPClient.cs
│   ├── MCPContextMapper.cs
│   └── MCPConfig.cs
│
└── /Examples
    ├── ListRepositoriesExample.cs
    ├── CreateIssueExample.cs
    └── MCPIntegrationExample.cs

## Dotnet Commands

dotnet new console --force
mkdir Models Services Core Examples
dotnet add package Microsoft.Extensions.Http
dotnet add package Microsoft.Extensions.Logging.Console --version 9.0.0-preview.3.*
dotnet add package Polly --version 8.4.2
dotnet add package System.Net.Http.Json --version 9.0.0
dotnet add package Octokit (Optional)

dotnet list package (Verify packages)

export GITHUB_TOKEN="your_token_here" (Linux)
$env:GITHUB_TOKEN="your_token_here" (Windows)

# List your repos (and print to MCP logger)
dotnet run -- list-repos

# Create a new private repo
dotnet run -- create-repo MyNewRepo true

# Delete a repo
dotnet run -- delete-repo your-gh-user MyNewRepo

# List issues
dotnet run -- issues your-gh-user some-repo

# Create an issue
dotnet run -- new-issue your-gh-user some-repo "Bug: login fails" "Steps to reproduce..."

# List PRs
dotnet run -- prs your-gh-user some-repo

# Create a PR (source -> target)
dotnet run -- new-pr your-gh-user some-repo feature/login main "Add login screen"

# List workflows
dotnet run -- workflows your-gh-user some-repo

# Dispatch a workflow on branch 'main'
dotnet run -- dispatch your-gh-user some-repo ci.yml main




