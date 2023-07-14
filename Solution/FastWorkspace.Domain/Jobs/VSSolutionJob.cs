using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class VSSolutionJob : BaseJob
{
    public string SolutionFilePath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(SolutionFilePath))
        {
            return $"Start-Process -FilePath \"{SolutionFilePath}\"";
        }

        return string.Empty;
    }

    public override string GetDefaultName() => "Open Visual Studio Solution";
}
