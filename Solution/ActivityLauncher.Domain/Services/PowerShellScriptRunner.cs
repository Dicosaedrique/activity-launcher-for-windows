using ActivityLauncher.Domain.Common.Utils;
using ActivityLauncher.Domain.Exceptions;
using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;
using ActivityLauncher.Domain.Services.Declarations;
using System.Management.Automation;

namespace ActivityLauncher.Domain.Services;

public class PowerShellScriptRunner : IPowerShellScriptRunner
{
    private readonly IScriptGeneratorService _scriptGenerator;

    public PowerShellScriptRunner(IScriptGeneratorService scriptGenerator)
    {
        _scriptGenerator = scriptGenerator;
    }

    public async Task<Result> RunScript(IScriptable scriptable)
    {
        var script = scriptable.GetScript();

        if (!string.IsNullOrWhiteSpace(script))
        {
            return await RunScript(script);
        }

        return new PowerShellRunException("Script cannot be null!").AsResult();
    }

    public Task<Result> RunScript(Activity activity)
    {
        return RunScript(_scriptGenerator.GetScript(activity));
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
