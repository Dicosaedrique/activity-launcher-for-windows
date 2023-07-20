using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IPowerShellScriptRunner
{
    public Task<Result> RunScript(Workspace workspace);

    public Task<Result> RunScript(string script);
}
