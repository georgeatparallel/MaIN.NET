using MaIN.Core.Hub;
using MaIN.Domain.Configuration;
using MaIN.Domain.Models;

namespace Examples.Mcp;

public class ParallelSearchMcpExample(IMaINHub hub) : IExample
{
    public async Task Start()
    {
        Console.WriteLine("Parallel Search MCP example is running!");

        var result = await hub.Mcp()
            .WithBackend(BackendType.OpenAi)
            .WithConfig(new MaIN.Domain.Entities.Mcp
            {
                Name = "ParallelSearch",
                Command = "npx",
                Arguments =
                [
                    "-y", "mcp-remote@0.14.3", "https://search.parallel.ai/mcp",
                    "--transport", "http-only",
                    "--enable-proxy",
                    "--header", "User-Agent: MaIN.NET-ParallelSearchExample"
                ],
                Model = Models.OpenAi.Gpt4oMini
            })
            .PromptAsync("Search for the official .NET support policy, fetch the relevant Microsoft page, " +
                         "and summarize the difference between LTS and STS support. Include the source URL.");

        Console.WriteLine(result.Message.Content);
    }
}
