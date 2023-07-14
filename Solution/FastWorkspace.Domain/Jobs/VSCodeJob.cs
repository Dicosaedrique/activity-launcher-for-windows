using FastWorkspace.Domain.Utils;

namespace FastWorkspace.Domain.Jobs;

public class VSCodeJob : BaseJob
{
    public string FolderPath { get; set; } = string.Empty;

    public override string GetScript()
    {
        if (PathHelper.IsValidPath(FolderPath))
        {
            return $"code \"{FolderPath}\"";
        }

        return string.Empty;
    }
}
