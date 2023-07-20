using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IPowerShellScriptRunner
{
    public Task<Result> RunScript(Activity activity);

    public Task<Result> RunScript(string script);
}
