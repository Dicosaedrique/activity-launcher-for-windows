using Microsoft.JSInterop;

namespace ActivityLauncher.Client.Common.Services;

public class InteropService
{
    private readonly IJSRuntime _runtime;

    public InteropService(IJSRuntime runtime)
    {
        _runtime = runtime;
    }

    public async Task<string> GetHighlightedPowerShellScript(string script)
    {
        try
        {
            return await _runtime.InvokeAsync<string>("blazorInterop.getHighlightedPowerShellScript", script);
        }
        catch
        {
            return script;
        }
    }
}
