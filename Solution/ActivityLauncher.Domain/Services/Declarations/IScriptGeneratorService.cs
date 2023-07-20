using ActivityLauncher.Domain.Interfaces;
using ActivityLauncher.Domain.Model;

namespace ActivityLauncher.Domain.Services.Declarations;

public interface IScriptGeneratorService
{
    public string GetScript(Workspace workspace);
}
