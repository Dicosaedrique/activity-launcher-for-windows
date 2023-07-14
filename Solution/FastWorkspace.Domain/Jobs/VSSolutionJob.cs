using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class VSSolutionJob : BaseJob
{
    public string SolutionPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(SolutionPath))
        {
            return $"Start-Process -FilePath \"{SolutionPath}\"";
        }

        return string.Empty;
    }
}
