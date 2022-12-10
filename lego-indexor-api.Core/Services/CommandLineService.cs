using CliWrap;
using CliWrap.Buffered;
using lego_indexor_api.Core.Interfaces.Services;

namespace lego_indexor_api.Core.Services;

public class CommandLineService : ICommandLineService
{
    public async Task<BufferedCommandResult> Run(string path, string args)
    {
        var result = await Cli.Wrap(path)
            .WithArguments(args)
            .ExecuteBufferedAsync();

        return result;
    }

    public async Task<BufferedCommandResult> RunPython(string args)
    {
        var result = await Cli.Wrap("python3")
            .WithArguments(args)
            .ExecuteBufferedAsync();

        return result;
    }
}