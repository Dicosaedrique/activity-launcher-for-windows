using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Steps;

public class VSSolutionStep : BaseStep
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
