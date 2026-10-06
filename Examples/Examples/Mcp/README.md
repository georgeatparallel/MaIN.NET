# Parallel Search MCP example

Select **Parallel Search MCP** in the example runner to search the web, read a
source page and print a sourced answer through MaIN's `IMaINHub.Mcp()` API.

## Run

Install the .NET 10 SDK and Node.js 20 or newer (including `npx`). Configure the
OpenAI inference key using the existing `OPENAI_API_KEY` environment variable
or `MaIN:OpenAiKey` in your local `appsettings.json`. Keep your key out of Git.

From the repository root:

```sh
dotnet run --project Examples/Examples/Examples.csproj --framework net10.0
```

Choose **Parallel Search MCP** from the menu. The prompt searches for Microsoft's
.NET support policy, fetches the source page and asks for an explanation with its
URL. Inference still uses your OpenAI account and incurs its normal charges.

## Configuration

The example passes a `MaIN.Domain.Entities.Mcp` configuration to
`IMaINHub.Mcp().WithBackend(...).WithConfig(...).PromptAsync(...)`. MaIN's MCP
client uses stdio, so `npx -y mcp-remote@0.14.3` bridges it to
`https://search.parallel.ai/mcp` using HTTP. The first run downloads the pinned
bridge from npm; later runs can use npm's cache. The bridge sends
`User-Agent: MaIN.NET-ParallelSearchExample` on remote MCP requests.
It also honors `HTTP_PROXY`, `HTTPS_PROXY` and `NO_PROXY` when your network uses a
proxy; without these variables it connects directly.

[Parallel Search MCP](https://docs.parallel.ai/integrations/mcp/search-mcp) exposes
`web_search` and `web_fetch`. The anonymous endpoint is free for light use with
lower rate limits and needs no Parallel key, account or OAuth login. Anonymous
searches use Fast mode with server-managed settings. It isn't unlimited access.

This configuration is used only when you select this example. It doesn't change
the application's default backend, existing MCP configurations or other examples.
To reuse it, copy the `Mcp` initializer from `ParallelSearchMcpExample.cs` and select
your supported inference backend and model explicitly. The stdio configuration
format and result handling are the same as the other MCP examples.

If `npx` isn't found, add your Node.js installation to `PATH`. Connection errors
require network access to npm and `search.parallel.ai`; an inference authentication
error requires checking your OpenAI key. A Parallel rate-limit response means you
should wait before trying again.
