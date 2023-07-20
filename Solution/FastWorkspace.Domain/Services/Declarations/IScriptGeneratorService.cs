using FastWorkspace.Domain.Interfaces;
using FastWorkspace.Domain.Model;

namespace FastWorkspace.Domain.Services.Declarations;

public interface IScriptGeneratorService
{
    public string GetScript(Workspace workspace);

    public string GetScript(IJob job);
}
