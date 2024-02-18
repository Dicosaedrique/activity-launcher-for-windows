using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IScriptGeneratorService
{
    public string GetScript(Activity activity);
}
