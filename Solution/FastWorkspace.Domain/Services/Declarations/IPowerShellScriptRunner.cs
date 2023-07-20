using FastWorkspace.Domain.Common.Utils;
using FastWorkspace.Domain.Model;

namespace FastWorkspace.Domain.Services.Declarations;

public interface IPowerShellScriptRunner
{
    public Task<Result> RunScript(Workspace workspace);

    public Task<Result> RunScript(string script);
}
