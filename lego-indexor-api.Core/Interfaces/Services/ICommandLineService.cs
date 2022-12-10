using CliWrap.Buffered;

namespace lego_indexor_api.Core.Interfaces.Services;

public interface ICommandLineService
{
    Task<BufferedCommandResult> Run(string path, string args);
    Task<BufferedCommandResult> RunPython(string args);
}