using System.Management.Automation;
using FastWorkspace.Domain.Common.Utils;
using FastWorkspace.Domain.Exceptions;
using FastWorkspace.Domain.Model;
using FastWorkspace.Domain.Services.Declarations;

namespace FastWorkspace.Domain.Services;

public class PowerShellScriptRunner : IPowerShellScriptRunner
{
    private readonly IScriptGeneratorService _scriptGenerator;

    public PowerShellScriptRunner(IScriptGeneratorService scriptGenerator)
    {
        _scriptGenerator = scriptGenerator;
    }

    public Task<Result> RunScript(Workspace workspace)
    {
        return RunScript(_scriptGenerator.GetScript(workspace));
    }

    public async Task<Result> RunScript(string script)
    {
        Exception? exception = null;

        try
        {
            var powershell = PowerShell.Create().AddScript(script);
            await powershell.InvokeAsync();
            var errors = powershell.Streams.Error.ToList();

            if (errors.Any())
            {
                exception = new PowerShellRunException(errors);
            }
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        return exception == null ? Result.SuccessResult : exception.AsResult();
    }
}
